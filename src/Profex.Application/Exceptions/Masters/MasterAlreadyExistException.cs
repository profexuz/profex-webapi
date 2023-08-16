namespace Profex.Application.Exceptions.Masters
{
    public class MasterAlreadyExistException : AlreadyExistsException
    {
        public MasterAlreadyExistException()
        {
            this.TitleMessage ="Master already is exists";
        }

        public MasterAlreadyExistException(string phone)
        {
            this.TitleMessage = "This is number is already registered";
        }

    }
}
