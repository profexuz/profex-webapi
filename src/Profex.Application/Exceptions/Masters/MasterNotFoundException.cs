namespace Profex.Application.Exceptions.Masters;

public class MasterNotFoundException : NotFoundException
{
    public MasterNotFoundException()
    {
        this.TitleMessage = "Master not found exception";
    }
}
