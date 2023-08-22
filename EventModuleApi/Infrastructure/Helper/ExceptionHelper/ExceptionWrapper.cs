using System.Net;
using EventModuleApi.Core.Contracts;

namespace EventModuleApi.Infrastructure.Helper;
public class ExceptionWrapper<T>
{
    private readonly ILoggerService _logger;
    public ExceptionWrapper(ILoggerService _logger)
    {
        this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
    }
    public async Task<ServiceResponse<T>> CallMethodAsync(Func<Task<ServiceResponse<T>>> method)
    {
        ServiceResponse<T> response = new()
        {
            Data = default,
            IsError = true
        };

        try
        {
            response = await method();
        }
        catch (CustomException ex)
        {
            response.StatusCode = (HttpStatusCode)ex.GetStatusCode();
            response.Message = ex.Message;
            response.Description = ex.StackTrace;
            _logger.LogWarning(ex.Message, ex.Source ?? ex.StackTrace);

        }
        catch (Exception ex)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Message = ex.Message;
            response.Message = ex.StackTrace ?? ex.Source;
            _logger.LogError(ex.Message, ex.Source ?? ex.StackTrace);

        }
        return response;
    }

    public async Task<PagedResponse<T>> CallMethodAsync(Func<Task<PagedResponse<T>>> method)
    {
        PagedResponse<T> response = new()
        {
            IsError = true,
        };

        try
        {
            response = await method();
        }
        catch (CustomException ex)
        {
            response.StatusCode = ex.GetStatusCode();
            response.Message = ex.Message;
            response.Description = ex.StackTrace;
            _logger.LogWarning(ex.Message, ex.Source ?? ex.StackTrace);


        }
        catch (Exception ex)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Message = ex.Message;
            response.Description = ex.StackTrace ?? ex.Source;
            _logger.LogError(ex.Message, ex.Source ?? ex.StackTrace);

        }
        return response;
    }
}
