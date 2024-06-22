using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Infrastructure.Caching;

public static class CacheOption
{
    public static DistributedCacheEntryOptions Create(TimeSpan? expiration = null) => 
        expiration is null
        ? new DistributedCacheEntryOptions {AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)}
        : new DistributedCacheEntryOptions {AbsoluteExpirationRelativeToNow = expiration};
}