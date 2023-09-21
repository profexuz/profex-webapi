using FluentValidation;
using Profex.Persistance.Dtos.Skills;

namespace Profex.Persistance.Validations.Dtos.Skills
{
    public class SkillUpdateValidator : AbstractValidator<SkillUpdateDto>
    {
        public SkillUpdateValidator()
        {
            RuleFor(dto => dto.CategoryId)
                .NotEmpty().NotNull().WithMessage("Category id is required!")
                .GreaterThanOrEqualTo(0).WithMessage("Id should be greater than or equal to zero");

            RuleFor(dto => dto.Description).NotEmpty().NotNull().WithMessage("Description is required!")
            .MaximumLength(20).WithMessage("Description is less be than 20 characters")
            .MinimumLength(5).WithMessage("Description is must be than 5 characters");

            RuleFor(dto => dto.Title).NotEmpty().NotNull().WithMessage("Title is required!")
                .MaximumLength(20).WithMessage("Title is less be than 20 characters")
                .MinimumLength(5).WithMessage("Title is must be than 5 characters");
        }
    }
}
