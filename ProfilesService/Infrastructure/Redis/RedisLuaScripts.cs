namespace Infrastructure.Redis;

public static class RedisLuaScripts
{
    /// <summary>
    /// Try to acquire N permits.
    /// Return 1 if acquired, 0 otherwise.
    /// </summary>
    public const string AcquireScript = @"
local current = redis.call('GET', KEYS[1])
if (not current) then
  redis.call('SET', KEYS[1], ARGV[1])
  current = ARGV[1]
end

if (tonumber(current) >= tonumber(ARGV[2])) then
  redis.call('DECRBY', KEYS[1], ARGV[2])
  return 1
end

return 0
";

    /// <summary>
    /// Release N permits.
    /// Always returns 1.
    /// </summary>
    public const string ReleaseScript = @"
redis.call('INCRBY', KEYS[1], ARGV[1])
return 1
";
}