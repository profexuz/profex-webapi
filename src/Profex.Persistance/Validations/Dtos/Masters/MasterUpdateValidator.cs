using FluentValidation;
using Profex.Persistance.Dtos.Master1;
using Profex.Service.Helpers;

namespace Profex.Persistance.Validations.Dtos.Masters;

public class MasterUpdateValidator : AbstractValidator<Master1UpdateDto>
{
    public MasterUpdateValidator()
    {
        RuleFor(dto => dto.FirstName).NotEmpty().NotNull().WithMessage("First name is required!")
        .MaximumLength(20).WithMessage("First name lass be than 20 characters")
        .MinimumLength(3).WithMessage("First name must be than 3 characters");

        RuleFor(dto => dto.LastName).NotEmpty().NotNull().WithMessage("Last name is required!")
            .MaximumLength(20).WithMessage("Last name lass be than 20 characters")
            .MinimumLength(3).WithMessage("Last name must be than 3 characters");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");
       
        When(dto => dto.ImagePath is not null, () =>
        {
            int maxImageSizeMB = 3;
            RuleFor(dto => dto.ImagePath!.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
            RuleFor(dto => dto.ImagePath!.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not image file");
        });
    }
}
