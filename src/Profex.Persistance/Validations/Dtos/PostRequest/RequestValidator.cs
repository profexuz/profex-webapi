using FluentValidation;
using Profex.Persistance.Dtos.PostRequest;

namespace Profex.Persistance.Validations.Dtos.PostRequest;

public class RequestValidator : AbstractValidator<RequestDto>
{
    public RequestValidator()
    {
        RuleFor(dto => dto.PostId).NotEmpty().NotNull().WithMessage("Post id is required");
        RuleFor(dto => dto.UserId).NotEmpty().NotNull().WithMessage("User id is required");
    }
}
