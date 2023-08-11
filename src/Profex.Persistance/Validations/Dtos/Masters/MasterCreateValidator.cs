using FluentValidation;
using Profex.Persistance.Dtos.Masters;
using Profex.Service.Helpers;

namespace Profex.Persistance.Validations.Dtos.Masters;

public class MasterCreateValidator : AbstractValidator<MasterCreateDto>
{
    public MasterCreateValidator()
    {
        RuleFor(dto => dto.FirstName).NotEmpty().NotNull().WithMessage("First name is required!")
            .MaximumLength(20).WithMessage("First name lass be than 20 characters")
            .MinimumLength(3).WithMessage("First name must be than 3 characters");

        RuleFor(dto => dto.LastName).NotEmpty().NotNull().WithMessage("Last name is required!")
            .MaximumLength(20).WithMessage("Last name lass be than 20 characters")
            .MinimumLength(3).WithMessage("Last name must be than 3 characters");

        RuleFor(dto => dto.Phone_number).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        int maxImageSizeMB = 3;
        RuleFor(dto => dto.Image_path).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.Image_path.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.Image_path.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);

            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");

        RuleFor(dto => dto.Password_hash).NotNull().NotEmpty().WithMessage("Password is requuired")
            .Must(password => PasswordValidator.IsStrongPassword(password).IsValid).WithMessage("Password is not strong password!");
    }
}