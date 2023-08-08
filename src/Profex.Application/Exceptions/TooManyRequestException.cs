using System.Net;

namespace Profex.Application.Exceptions;

public class TooManyRequestException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.TooManyRequests;
    public string TitleMessage { get; protected set; } = string.Empty;
}
