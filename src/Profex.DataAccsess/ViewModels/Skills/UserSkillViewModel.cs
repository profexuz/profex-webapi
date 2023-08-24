using Profex.Domain.Entities;

namespace Profex.DataAccsess.ViewModels.Skills
{
    public class UserSkillViewModel : Auditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public bool PhoneNumberConfirmed { get; set; }

        public string ImagePath = string.Empty;     
        public bool IsFree { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
