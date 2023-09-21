﻿using FluentValidation;
using Profex.Persistance.Dtos.PostImages;
using Profex.Service.Helpers;

namespace Profex.Persistance.Validations.Dtos.PostImages;

public class PostImageValidator : AbstractValidator<PostImageCreateDto> 
{
    public PostImageValidator()
    {
        RuleFor(dto=>dto.PostId)
            .NotEmpty().NotNull().WithMessage("Post id is required!")
            .GreaterThanOrEqualTo(0).WithMessage("Id should be greater than or equal to zero");


        int maxImageSizeMB = 3;
        RuleFor(dto => dto.ImagePath).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.ImagePath.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.ImagePath.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);

            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
       
    }
}
