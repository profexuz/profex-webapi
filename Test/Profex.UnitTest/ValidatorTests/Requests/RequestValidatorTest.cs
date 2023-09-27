using Profex.Persistance.Dtos.PostRequest;
using Profex.Persistance.Validations.Dtos.PostRequest;

namespace Profex.UnitTest.ValidatorTests.Requests;

public class RequestValidatorTest
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 0)]
    [InlineData(-2, 0)]
    [InlineData(-3, 1)]
    [InlineData(-4, 0)]
    [InlineData(0, 0)]
    
    public void ShouldReturnInValidValidation(long postId, long userId)
    {
        RequestDto dto = new RequestDto()
        {
            PostId = postId,
            UserId = userId

        };
        var validator = new RequestValidator();
        var res = validator.Validate(dto);
        Assert.False(res.IsValid);
    }
}
