using FluentValidation;
using Profex.Persistance.Dtos.Categories;

namespace Profex.Persistance.Validations.Dtos.Categories
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(40).WithMessage("Name must be less than 40 characters");

            RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
                .MinimumLength(5).WithMessage("Description field is required!");
        }
    }
}
