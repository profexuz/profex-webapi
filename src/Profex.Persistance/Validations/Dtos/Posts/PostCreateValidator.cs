using FluentValidation;
using Profex.Persistance.Dtos.Posts;

namespace Profex.Persistance.Validations.Dtos.Posts;

public class PostCreateValidator : AbstractValidator<PostCreateDto>
{
    public PostCreateValidator()
    {
        RuleFor(dto => dto.CategoryId)
            .NotEmpty().NotNull().WithMessage("Category id is required!")
            .GreaterThanOrEqualTo(0).WithMessage("Id should be greater than or equal to zero");

        RuleFor(dto => dto.Title).NotEmpty().NotNull().WithMessage("Title is required!")
            .MaximumLength(20).WithMessage("Title length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Title lenght must ber than 3 characters");

        RuleFor(dto => dto.Price).NotEmpty().WithMessage("Price is required!");

        RuleFor(dto => dto.Description).NotEmpty().NotNull().WithMessage("Description is required!")
            .MaximumLength(20).WithMessage("Description length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Description lenght must ber than 3 characters");

        RuleFor(dto => dto.Region).NotEmpty().NotNull().WithMessage("Region is required!")
            .MaximumLength(20).WithMessage("Region length lass be than 20 characters")
            .MinimumLength(3).WithMessage("Region lenght must ber than 3 characters");

        RuleFor(dto => dto.District).NotEmpty().NotNull().WithMessage("District is required!")
            .MaximumLength(20).WithMessage("District length lass be than 20 characters")
            .MinimumLength(3).WithMessage("District lenght must ber than 3 characters");

        RuleFor(dto => dto.Latidute).NotEmpty().NotNull().WithMessage("Lattidute is required!");

        RuleFor(dto => dto.Longitude).NotEmpty().NotNull().WithMessage("Longitude is required!");


        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Price).Must(price => PriceValidator.IsValid(price))
            .WithMessage("You entered the wrong price length or entered a negative number!");

        RuleFor(dto => dto.Latidute).Must(latitude => LatiduteValidator.IsValid(latitude))
            .WithMessage("you entered a negative number");

        RuleFor(dto => dto.Longitude).Must(longitude => LatiduteValidator.IsValid(longitude))
            .WithMessage("you entered a negative number");
    }
}
