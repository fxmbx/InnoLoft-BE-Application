namespace EventModuleApi.Core.Contracts;
public interface ILoggerService
{
    void LogInfo<T>(string messgae, T? data = default);
    void LogWarning<T>(string message, T? data = default);
    void LogError<T>(string message, T? data = default);
}