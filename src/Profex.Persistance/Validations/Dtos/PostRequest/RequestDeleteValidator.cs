using FluentValidation;
using Profex.Persistance.Dtos.PostRequest;

namespace Profex.Persistance.Validations.Dtos.PostRequest;

public class RequestDeleteValidator : AbstractValidator<RequestDeleteDto>
{
    public RequestDeleteValidator()
    {
        RuleFor(dto => dto.masterId)
           .NotEmpty().NotNull().WithMessage("Master id is required")
           .GreaterThanOrEqualTo(0).WithMessage("Master id should be greater than or equal to zero");

        RuleFor(dto => dto.postId)
            .NotEmpty().NotNull().WithMessage("Post id is required")
            .GreaterThanOrEqualTo(0).WithMessage("Post id should be greater than or equal to zero");

        RuleFor(dto => dto.userId)
           .NotEmpty().NotNull().WithMessage("Master id is required")
           .GreaterThanOrEqualTo(0).WithMessage("Master id should be greater than or equal to zero");
    }
}
