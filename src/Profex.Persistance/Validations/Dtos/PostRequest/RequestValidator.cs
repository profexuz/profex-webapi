using FluentValidation;
using Profex.Persistance.Dtos.PostRequest;

namespace Profex.Persistance.Validations.Dtos.PostRequest;

public class RequestValidator : AbstractValidator<RequestDto>
{
    public RequestValidator()
    {
        RuleFor(dto => dto.PostId)
            .NotEmpty().NotNull().WithMessage("Post id is required")
            .GreaterThanOrEqualTo(0).WithMessage("Id should be greater than or equal to zero");

        RuleFor(dto => dto.UserId)
            .NotEmpty().NotNull().WithMessage("User id is required")
            .GreaterThanOrEqualTo(0).WithMessage("Id should be greater than or equal to zero");

    }
}
