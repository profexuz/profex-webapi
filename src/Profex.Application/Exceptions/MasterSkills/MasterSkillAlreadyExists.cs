namespace Profex.Application.Exceptions.MasterSkills;

public class MasterSkillAlreadyExists : AlreadyExistsException
{
    public MasterSkillAlreadyExists()
    {
        this.TitleMessage = "Master already has this skill";
    }
}
