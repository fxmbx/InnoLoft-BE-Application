using System.Net;
using System.Runtime.Serialization;

namespace EventModuleApi.Infrastructure.Helper;
[Serializable]
public class CustomException : Exception
{
    private HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

    public CustomException(string message, HttpStatusCode code)
        : base(message)
    {
        SetStatusCode(code);
    }

    protected CustomException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }

    public void SetStatusCode(HttpStatusCode code)
    {
        statusCode = code;
    }
    public HttpStatusCode GetStatusCode()
    {
        return statusCode;
    }

}