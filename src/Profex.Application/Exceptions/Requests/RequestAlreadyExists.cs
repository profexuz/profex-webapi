namespace Profex.Application.Exceptions.Requests;

public class RequestAlreadyExists : AlreadyExistsException
{
    public RequestAlreadyExists()
    {
        TitleMessage = "You Already requested ";
    }
}
