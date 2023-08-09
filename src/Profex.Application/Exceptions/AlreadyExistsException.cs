using System.Net;

namespace Profex.Application.Exceptions;

public class AlreadyExistsException : Exception
{
    public HttpStatusCode StatusCode = HttpStatusCode.Conflict;
    public string TitleMessage { get; protected set; }= string.Empty;
}
