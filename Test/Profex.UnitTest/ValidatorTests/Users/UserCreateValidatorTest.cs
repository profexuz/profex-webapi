using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Profex.Persistance.Dtos.Users;
using Profex.Persistance.Validations.Dtos.Users;
using System.Text;

namespace Profex.UnitTest.ValidatorTests.Users;

public class UserCreateValidatorTest
{
    [Theory]
    [InlineData(3.1)]
    [InlineData(3.01)]
    [InlineData(3.001)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void ShouldReturnWrongImageFileSize(double imageSizeMB)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(imageSizeMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        UserCreateDto userCreateDto = new UserCreateDto()
        {
            FirstName = "ozodbek",
            LastName = "Jumaqulov",
            PhoneNumber = "+998901234567",
            PhoneNumberConfirmed = true,
            ImagePath = imageFile,
            PasswordHash = "AAaa11@@",
            Salt = "AAaa@@11"
        };
        var validator = new UserCreateValidator();
        var result = validator.Validate(userCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
    [InlineData("!23aADAdsddsds!@#!@#!@3adsdasd")]
    [InlineData("!@#QEad123!@#!@#1243123")]
    [InlineData("A1!@#!#qwesd@#QEsdfsdfsdf")]
    [InlineData("A1!@#!#qwasdaesd@#QEsdfsdfsdf")]
    [InlineData("A1!@#!asdasd#qwesd@#QEsdfsdfsdf")]
    [InlineData("aAdaE!@qeqWE!@#!@#adsadssa1")]
    [InlineData("A0as12312da!@#!@#sdsdfsdfsdf")]
    [InlineData("       dfasdfsdfsdfa12")]
    [InlineData("       2323411!@#!@aadsfASDA")]
    [InlineData("       ASD123!@#!@aadsfASDA")]
    [InlineData("ASD123!@#!@aadsfASDA      asd ")]
    [InlineData("ASD123!@#!@aadsfASDsdfsA   assdasdas")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
       "products to our clients")]
    public void ShouldReturnInValidValidation(string name)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(3 * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        UserCreateDto userCreateDto = new UserCreateDto()
        {
            FirstName = name,
            LastName = "Example",
            PhoneNumber= "+998770079639",
            PhoneNumberConfirmed = true,
            ImagePath = imageFile,
            PasswordHash = "AAaa11##"
        };
        var validator = new UserCreateValidator();
        var result = validator.Validate(userCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
    [InlineData("1234dsfsdfAsdfsdfasdDASD@#!@#!@#adsasd")]
    [InlineData("1234dsdfsdfsfsdfADASD@#!@#!@#adsasd")]
    [InlineData("1234dsdfsasddfsfsdfADASasdasdD@#!@#!@#adsasd")]
    [InlineData("1234dsdsdasdfsdfsfsdfADASD@#!@#!@#adsasd")]
    [InlineData("123asda4dsdasdfsdfsfsdfADASD@#!@#!@#adsasd")]
    [InlineData("!23aADAdsddsds!@#!@#!@3adsdasd")]
    [InlineData("!@#QEad123!@#!@#1243123")]
    [InlineData("A1!@#!#qwesd@#QEsdfsdfsdf")]
    [InlineData("aAdaE!@qeqWE!@#!@#adsadssa1")]
    [InlineData("A0as12312da!@#!@#sdsdfsdfsdf")]
    [InlineData("       dfasdfsdfsdfa12")]
    [InlineData("       2323411!@#!@aadsfASDA")]
    [InlineData("       ASD123!@#!@aadsfASDA")]
    [InlineData("ASD123!@#!@aadsfASDA      asd ")]
    [InlineData("ASD123!@#!@aadsfASDsdfsA   assdasdas")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
       "products to our clients")]
    public void Should1ReturnInValidValidation(string lastname)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(3 * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        UserCreateDto userCreateDto = new UserCreateDto()
        {
            FirstName = "Example",
            LastName = lastname,
            PhoneNumber = "+998770079639",
            PhoneNumberConfirmed = true,
            ImagePath = imageFile,
            PasswordHash = "AAaa11##"
        };
        var validator = new UserCreateValidator();
        var result = validator.Validate(userCreateDto);
        Assert.False(result.IsValid);
    }
}
