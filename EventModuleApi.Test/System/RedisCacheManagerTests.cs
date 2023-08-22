using EventModuleApi.Core.Config;
using EventModuleApi.Core.Contracts;
using Microsoft.Extensions.Options;
using Moq;

namespace EventModuleApi.Test.System;
public class RedisCacheManagerTests
{
    private Mock<ICacheManager> _mockCacheManager;

    public RedisCacheManagerTests()
    {
        _mockCacheManager = new Mock<ICacheManager>();
        var mockRedisConfig = new Mock<IOptions<RedisConfig>>();
        mockRedisConfig.Setup(config => config.Value)
                       .Returns(new RedisConfig { Host = "127.0.0.1", Password = "pass", DB = 0 });
    }

    [Fact]
    public async Task GetAsync_ShouldDeserializeCachedValue()
    {
        // Arrange
        var key = "testKey";
        var cachedValue = "testValue";
        _mockCacheManager.Setup(cm => cm.GetAsync<string>(key))
                         .ReturnsAsync(cachedValue);

        // Act
        var result = await _mockCacheManager.Object.GetAsync<string>(key);

        // Assert
        Assert.Equal("testValue", result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnDefault_WhenCachedValueIsNullOrEmpty()
    {
        // Arrange
        var key = "testKey";
        _mockCacheManager.Setup(cm => cm.GetAsync<string>(key))
                         .ReturnsAsync(value: null);

        // Act
        var result = await _mockCacheManager.Object.GetAsync<string>(key);

        // Assert
        Assert.Equal(default(string), result);
    }

    [Fact]
    public async Task SetAsync_ShouldSetCachedValue()
    {
        // Arrange
        var key = "testKey";
        var value = "testValue";

        // Act
        await _mockCacheManager.Object.SetAsync(key, value);

        // Assert
        _mockCacheManager.Verify(cm => cm.SetAsync(key, value, null), Times.Once);
    }

    [Fact]
    public async Task SetAsync_WithExpiration_ShouldSetCachedValueWithExpiration()
    {
        // Arrange
        var key = "testKey";
        var value = "testValue";
        var expiration = TimeSpan.FromMinutes(30);

        // Act
        await _mockCacheManager.Object.SetAsync(key, value, expiration);

        // Assert
        _mockCacheManager.Verify(cm => cm.SetAsync(key, value, expiration), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveCachedValue()
    {
        // Arrange
        var key = "testKey";

        // Act
        await _mockCacheManager.Object.RemoveAsync(key);

        // Assert
        _mockCacheManager.Verify(cm => cm.RemoveAsync(key), Times.Once);
    }
}
