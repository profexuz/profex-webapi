using FluentValidation;
using Profex.Persistance.Dtos.MasterSkill;

namespace Profex.Persistance.Validations.Dtos.MasterSkill
{
    public class MasterSkillUpdateValidator : AbstractValidator<MasterSkillUpdateDto>
    {
        public MasterSkillUpdateValidator()
        {
            RuleFor(dto => dto.MasterId)
                .NotEmpty().WithMessage("MasterId is required!")
                .GreaterThanOrEqualTo(0).WithMessage("Id should be greater than or equal to zero");

            RuleFor(dto => dto.SkillId)
                .NotEmpty().WithMessage("SkillId is required!")
                .GreaterThanOrEqualTo(0).WithMessage("Id should be greater than or equal to zero");
        }
    }
}
