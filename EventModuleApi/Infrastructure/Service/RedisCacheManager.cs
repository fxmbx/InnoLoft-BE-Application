
using System.Text.Json;
using EventModuleApi.Core.Config;
using EventModuleApi.Core.Contracts;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace EventModuleApi.Infrastructure.Service;
public class RedisCacheManager : ICacheManager
{
    private readonly IDatabase _redisDatabase;
    public RedisCacheManager(IOptions<RedisConfig> _redisConfig)
    {
        var redisoptions = ConfigurationOptions.Parse(_redisConfig.Value.Host!);
        redisoptions.Password = _redisConfig.Value.Password!;
        var multiplexer = ConnectionMultiplexer.Connect(redisoptions);
        _redisDatabase = multiplexer.GetDatabase(_redisConfig.Value.DB!);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        string? cachedValue = await _redisDatabase.StringGetAsync(key);
        if (!string.IsNullOrEmpty(cachedValue))
        {
            return JsonSerializer.Deserialize<T>(cachedValue);
        }
        return default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {

        string serializedValue = JsonSerializer.Serialize(value);
        if (expiration == null)
        {
            await _redisDatabase.StringSetAsync(key, serializedValue);
        }
        else
        {
            await _redisDatabase.StringSetAsync(key, serializedValue, expiration);
        }
    }

    public async Task RemoveAsync(string key)
    {
        await _redisDatabase.KeyDeleteAsync(key);
    }
}
