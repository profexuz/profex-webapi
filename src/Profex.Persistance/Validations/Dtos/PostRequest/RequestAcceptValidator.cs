using FluentValidation;
using Profex.Persistance.Dtos.PostRequest;

namespace Profex.Persistance.Validations.Dtos.PostRequest;

public class RequestAcceptValidator : AbstractValidator<RequestAcceptDto>
{
    public RequestAcceptValidator()
    {
        RuleFor(dto => dto.masterId).NotEmpty().NotNull().WithMessage("Master id is required");
        RuleFor(dto => dto.postId).NotEmpty().NotNull().WithMessage("Post id is required");
    }
}
