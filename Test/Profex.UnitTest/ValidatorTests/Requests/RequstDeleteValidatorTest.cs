using Profex.Persistance.Dtos.PostRequest;
using Profex.Persistance.Validations.Dtos.PostRequest;

namespace Profex.UnitTest.ValidatorTests.Requests;

public class RequstDeleteValidatorTest
{
    [Theory]
    [InlineData(0,1,-1)]
    [InlineData(-1,-1,0)]
    [InlineData(-2,0,2)]
    [InlineData(-3,1,0)]
    [InlineData(-4,0,-5)]

    public void ShouldReturnInValidValidation(long postId, long masterId, long userId)
    {
        RequestDeleteDto dto = new RequestDeleteDto()
        {
            postId = postId,
            masterId = masterId,
            userId = userId
        };
        var validator = new RequestDeleteValidator();
        var res = validator.Validate(dto);
        Assert.False(res.IsValid);
    }
}
