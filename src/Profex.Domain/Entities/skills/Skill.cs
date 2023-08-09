namespace Profex.Domain.Entities.skills;

public class Skill : Auditable
{
    public long CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}
