using FluentValidation;
using Profex.Persistance.Dtos.Categories;

namespace Profex.Persistance.Validations.Dtos.Categories;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(30).WithMessage("Name must be less than 30 characters");

        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
            .MinimumLength(5).WithMessage("Description field is required!")
            .MaximumLength(200).WithMessage("Must be less than 200 characters");
    }
}
