using FluentValidation;
using Profex.Persistance.Dtos.MasterSkill;

namespace Profex.Persistance.Validations.Dtos.MasterSkill
{
    public class MasterSkillCreateValidator : AbstractValidator<MasterSkillCreateDto>
    {
        public MasterSkillCreateValidator()
        {
            RuleFor(dto => dto.MasterId).NotEmpty().WithMessage("MasterId is required!");

            RuleFor(dto => dto.SkillId).NotEmpty().WithMessage("SkillId is required!");
        }
    }
}
