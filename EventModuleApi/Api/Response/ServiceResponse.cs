using System.Net;

namespace EventModuleApi.Response;
public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; } = "Success";
    public string? Description { get; set; } 
    public HttpStatusCode StatusCode { get; set; }
    public bool  IsError { get; set; } = false;
}
