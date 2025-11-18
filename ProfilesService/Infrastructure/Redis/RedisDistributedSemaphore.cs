using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Synchronization;
using StackExchange.Redis;

namespace Infrastructure.Redis;

public class RedisDistributedSemaphore : IDistributedSemaphore
{
    private readonly IDatabase _db;
    private readonly TimeSpan _retryDelay = TimeSpan.FromMilliseconds(200);

    public RedisDistributedSemaphore(IConnectionMultiplexer connection)
    {
        _db = connection.GetDatabase();
    }

    public async Task<bool> WaitAsync(
        string key,
        int permits,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        if (permits <= 0)
            throw new ArgumentException("Permits must be > 0");

        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = (int) (await _db.ScriptEvaluateAsync(
                RedisLuaScripts.AcquireScript,
                keys: new RedisKey[] { key },
                values: new RedisValue[] { int.MaxValue, permits } // ARGV[1], ARGV[2]
            ));

            if (result == 1)
                return true;

            await Task.Delay(_retryDelay, cancellationToken);
        }

        return false;
    }

    public async Task ReleaseAsync(
        string key,
        int permits,
        CancellationToken cancellationToken = default)
    {
        if (permits <= 0)
            throw new ArgumentException("Permits must be > 0");

        _ = await _db.ScriptEvaluateAsync(
            RedisLuaScripts.ReleaseScript,
            keys: new RedisKey[] { key },
            values: new RedisValue[] { permits }
        );
    }
}