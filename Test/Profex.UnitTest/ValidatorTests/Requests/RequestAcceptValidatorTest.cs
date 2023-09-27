using Profex.Persistance.Dtos.PostRequest;
using Profex.Persistance.Validations.Dtos.PostRequest;

namespace Profex.UnitTest.ValidatorTests.Requests;

public class RequestAcceptValidatorTest
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 0)]
    [InlineData(-2, 0)]
    [InlineData(-3, 1)]
    [InlineData(-4, 0)]
    [InlineData(0, 0)]
   
    public void ShouldReturnInValidValidation(long postId, long masterId)
    {
        RequestAcceptDto dto = new RequestAcceptDto()
        {
            postId = postId,
            masterId = masterId
            
        };
        var validator = new RequestAcceptValidator();
        var res = validator.Validate(dto);
        Assert.False(res.IsValid);
    }
}
