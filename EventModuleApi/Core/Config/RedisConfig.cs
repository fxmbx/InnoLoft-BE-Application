namespace EventModuleApi.Core.Config;
public class RedisConfig
{
    public string? Host { get; set; }
    public string? Password { get; set; }
    public int DB { get; set; } = -1;
}
