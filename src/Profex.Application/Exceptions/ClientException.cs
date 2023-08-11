using System.Net;

namespace Profex.Application.Exceptions;

public abstract class ClientException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    public abstract string TitleMessage { get; protected set; }
}
