﻿using FluentValidation;
using Profex.Persistance.Dtos.AdminAuth;

namespace Profex.Persistance.Validations.Dtos.Admin
{
    public class RegisterValdiator : AbstractValidator<RegisterAdminDto>
    {
        public RegisterValdiator()
        {
            RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MinimumLength(3).WithMessage("Firstname must be more than 3 characters")
            .MaximumLength(30).WithMessage("Firstname must be less than 30 characters");
            
            RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
                .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

            RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
                .WithMessage("Password is not strong password!");
        }
    }
}
