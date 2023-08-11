using FluentValidation;
using Profex.Persistance.Dtos.Posts;

namespace Profex.Persistance.Validations.Dtos.Posts;

public class PostCreateValidator : AbstractValidator<PostCreateDto>
{
    public PostCreateValidator()
    {
        RuleFor(dto => dto.Title).NotEmpty().NotNull().WithMessage("Title is required!")
            .MinimumLength(20).WithMessage("Title length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Title lenght must ber than 3 characters");

        RuleFor(dto => dto.Price).NotEmpty().NotNull().WithMessage("Price is required!")
            .MinimumLength(20).WithMessage("Price length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Price lenght must ber than 3 characters");

        RuleFor(dto => dto.Description).NotEmpty().NotNull().WithMessage("Description is required!")
            .MinimumLength(20).WithMessage("Description length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Description lenght must ber than 3 characters");

        RuleFor(dto => dto.Region).NotEmpty().NotNull().WithMessage("Region is required!")
            .MinimumLength(20).WithMessage("Region length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Region lenght must ber than 3 characters");

        RuleFor(dto => dto.District).NotEmpty().NotNull().WithMessage("District is required!")
            .MinimumLength(20).WithMessage("District length lass be than 20 characters")
            .MinimumLength(3).WithMessage("District lenght must ber than 3 characters");

        RuleFor(dto => dto.Lattidute).NotEmpty().NotNull().WithMessage("Lattidute is required!")
            .MinimumLength(20).WithMessage("Lattidute length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Lattidute lenght must ber than 3 characters");
        
        RuleFor(dto => dto.Longitude).NotEmpty().NotNull().WithMessage("Longitude is required!")
            .MinimumLength(20).WithMessage("Longitude length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Longitude lenght must ber than 3 characters");

        RuleFor(dto => dto.Phone_number).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

    }
}
