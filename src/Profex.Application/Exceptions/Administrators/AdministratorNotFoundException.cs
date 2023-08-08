namespace Profex.Application.Exceptions.Administsrators;

public class AdministratorNotFoundException : NotFoundException
{
    public AdministratorNotFoundException()
    {
        this.TitleMessage = "Administrator not found";
    }
}
