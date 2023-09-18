namespace Profex.Application.Exceptions.Requests;

public class RequestNotFoundException : NotFoundException
{
    public RequestNotFoundException()
    {
        this.TitleMessage = "Request not found";
    }
}
