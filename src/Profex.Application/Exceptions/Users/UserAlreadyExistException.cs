namespace Profex.Application.Exceptions.Users;

public class UserAlreadyExistException : AlreadyExistsException
{
    public UserAlreadyExistException()
    {
        TitleMessage = "User already exists";
    }

    public UserAlreadyExistException(string phone)
    {
        TitleMessage = "This phone is already registered";
    }
}
