namespace Profex.Persistance.Dtos.Skills;

public class SkillUpdateDto
{
    public long CategoryId { get; set; }
    public string Description { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
}
