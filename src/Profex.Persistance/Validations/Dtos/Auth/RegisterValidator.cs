using FluentValidation;
using Profex.Persistance.Dtos.Auth;

namespace Profex.Persistance.Validations.Dtos.Auth;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MinimumLength(3).WithMessage("Firstname must be more than 3 characters")
            .MaximumLength(30).WithMessage("Firstname must be less than 30 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MinimumLength(3).WithMessage("Lastname must be more than 3 characters")
            .MaximumLength(30).WithMessage("Lastname must be less than 30 characters");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Password is not strong password!");
    }
}
