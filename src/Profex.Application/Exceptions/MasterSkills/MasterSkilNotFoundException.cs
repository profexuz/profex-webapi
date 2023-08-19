namespace Profex.Application.Exceptions.MasterSkills;

public class MasterSkilNotFoundException : NotFoundException
{
    public MasterSkilNotFoundException()
    {
        this.TitleMessage = "Master skill not found";
    }
}
