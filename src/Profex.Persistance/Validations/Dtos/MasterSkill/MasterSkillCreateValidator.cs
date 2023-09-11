using FluentValidation;
using Profex.Persistance.Dtos.MasterSkill;

namespace Profex.Persistance.Validations.Dtos.MasterSkill
{
    public class MasterSkillCreateValidator : AbstractValidator<MasterSkillCreateDto>
    {
        public MasterSkillCreateValidator()
        {
           
            RuleFor(dto => dto.SkillId).NotEmpty().NotNull().WithMessage("SkillId is required!");
        }
    }
}
