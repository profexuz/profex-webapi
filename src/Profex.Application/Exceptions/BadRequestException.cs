using System.Net;

namespace Profex.Application.Exceptions;

public class BadRequestException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;
    public string TitleMessage { get; protected set; } = string.Empty;
}
