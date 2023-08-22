using EventModuleApi.Core.Contracts;

namespace EventModuleApi.Service.Logger;
public class LoggerService : ILoggerService
{
    private readonly ILogger _logger;

    public LoggerService(ILogger<LoggerService> logger)
    {
        _logger = logger;
    }

    public void LogInfo<T>(string message, T? data = default) => _logger.LogInformation(message, data);

    public void LogWarning<T>(string message, T? data = default) => _logger.LogWarning(message, data);

    public void LogError<T>(string message, T? data = default) => _logger.LogError(message, data);
}
