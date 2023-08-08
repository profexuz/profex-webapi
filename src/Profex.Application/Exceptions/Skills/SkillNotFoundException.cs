namespace Profex.Application.Exceptions.Skills;

public class SkillNotFoundException : NotFoundException
{
    public SkillNotFoundException()
    {
        this.TitleMessage = "Skill not found";
    }
}
