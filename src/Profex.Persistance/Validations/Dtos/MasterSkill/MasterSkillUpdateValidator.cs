using FluentValidation;
using Profex.Persistance.Dtos.MasterSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profex.Persistance.Validations.Dtos.MasterSkill
{
    public class MasterSkillUpdateValidator : AbstractValidator<MasterSkillUpdateDto>
    {
        public MasterSkillUpdateValidator()
        {
            RuleFor(dto => dto.MasterId).NotEmpty().WithMessage("MasterId is required!");

            RuleFor(dto => dto.SkillId).NotEmpty().WithMessage("SkillId is required!");
        }
    }
}
