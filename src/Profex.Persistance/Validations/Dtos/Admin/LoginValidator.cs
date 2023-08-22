using FluentValidation;
using Profex.Persistance.Dtos.AdminAuth;

namespace Profex.Persistance.Validations.Dtos.Admin
{
    public class LoginValidator : AbstractValidator<AdminDto>
    {
        public LoginValidator()
        {
            RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
           .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

            RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
                .WithMessage("Password is not strong password!");
        }
    }
}
