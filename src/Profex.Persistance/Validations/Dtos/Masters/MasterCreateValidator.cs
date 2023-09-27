using FluentValidation;
using Profex.Persistance.Dtos.Masters;
using Profex.Service.Helpers;

namespace Profex.Persistance.Validations.Dtos.Masters;

public class MasterCreateValidator : AbstractValidator<MasterCreateDto>
{
    public MasterCreateValidator()
    {
        RuleFor(dto => dto.FirstName).NotEmpty().NotNull().WithMessage("First name is required!")
            .MaximumLength(30).WithMessage("First name lass be than 30 characters")
            .MinimumLength(3).WithMessage("First name must be than 3 characters");

        RuleFor(dto => dto.LastName).NotEmpty().NotNull().WithMessage("Last name is required!")
            .MaximumLength(30).WithMessage("Last name lass be than 30 characters")
            .MinimumLength(3).WithMessage("Last name must be than 3 characters");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        int maxImageSizeMB = 3;
        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);

            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");

        RuleFor(dto => dto.PasswordHash).NotNull().NotEmpty().WithMessage("Password is requuired")
            .Must(password => PasswordValidator.IsStrongPassword(password).IsValid).WithMessage("Password is not strong password!");
    
    }
}