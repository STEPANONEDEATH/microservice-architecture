using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Synchronization;

public interface IDistributedSemaphore
{
    Task<bool> WaitAsync(string key, int permits, TimeSpan timeout, CancellationToken cancellationToken = default);

    Task ReleaseAsync(string key, int permits, CancellationToken cancellationToken = default);
}