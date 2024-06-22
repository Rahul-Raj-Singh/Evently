using System.Runtime.Serialization;
using System.Text;
using Evently.Common.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Evently.Common.Infrastructure.Caching;

public class CacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
    {
        var valueAsString = await cache.GetStringAsync(cacheKey, cancellationToken);

        return valueAsString is null ? default(T) : JsonConvert.DeserializeObject<T>(valueAsString);
    }

    public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var valueAsString = JsonConvert.SerializeObject(value);

        await cache.SetStringAsync(cacheKey, valueAsString, CacheOption.Create(expiration), cancellationToken);
    }

    public async Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(cacheKey, cancellationToken);
    }

}