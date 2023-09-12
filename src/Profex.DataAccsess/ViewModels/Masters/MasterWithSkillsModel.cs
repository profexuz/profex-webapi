using Profex.Domain.Entities;
using Profex.Domain.Entities.skills;

namespace Profex.DataAccsess.ViewModels.Masters;

public class MasterWithSkillsModel : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public bool IsFree { get; set; }
    public IList<Skill> MasterSkills { get; set; } = new List<Skill>();
}
