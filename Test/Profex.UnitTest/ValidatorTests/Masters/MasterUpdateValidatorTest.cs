using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Profex.Persistance.Dtos.Masters;
using Profex.Persistance.Validations.Dtos.Masters;
using System.Text;

namespace Profex.UnitTest.ValidatorTests.Masters;

public class MasterUpdateValidatorTest
{
    [Theory]
    [InlineData(3.1)]
    [InlineData(3.01)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void ShouldReturnWrongImageFileSize(double imageSizeMB)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(imageSizeMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        MasterCreateDto masterCreateDto = new MasterCreateDto()
        {
            FirstName = "Ozodbek",
            LastName = "Jumaqulov",
            PhoneNumber = "+998770079639",
            ImagePath = imageFile,
            PasswordHash = "AAaa11@@"
        };
        var validator = new MasterCreateValidator();
        var result = validator.Validate(masterCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADAasdasdsadasdsaSD@#!@#!@#adsasd")]
    [InlineData("!23aADAdsddsds!sadsadsadsadsadsad@#!@#!@3adsdasd")]
    [InlineData("!@#QEad123!@#!@sadsadsadsadasdasdasdasdsad#1243123")]
    [InlineData("A1!@#!#qwesd@#QsadsadsadsadsadasdsadsadasdsadEsdfsdfsdf")]
    [InlineData("aAdaE!@qeqWE!@#sadasdsadsadasdsadasdasdsa!@#adsadssa1")]
    [InlineData("A0as12312da!@#!@#sdsdfsdfsdf 2312da!@#!@#sdsdfsdfsdf")]
    [InlineData("       dfasdfsdfsdfa12 2312da!@#!@#sdsdfsdfsdf")]
    [InlineData("       2323411!@#!@aadsfASDA 2312da!@#!@#sdsdfsdfsdf")]
    [InlineData("       ASD123!@#!@aadsfASDA 2312da!@#!@#sdsdfsdfsdf")]
    [InlineData("ASD123!@#!@aadsfASDA      asd2312da!@#!@#sdsdfsdfsdf ")]
    [InlineData("ASD123!@#!@aadsfASDsdfsA   assdasdas 2312da!@#!@#sdsdfsdfsdf")]
    [InlineData("       ASD123!@#!@aadsfASDA    2312da!@#!@#sdsdfsdfsdf  asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA   2312da!@#!@#sdsdfsdfsdf    asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
        "products to our clients")]
    public void ShouldReturnInValidValidation(string name)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(3 * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        MasterCreateDto masterCreateDto = new MasterCreateDto()
        {
            FirstName = name,
            LastName = "Jumaqulov",
            PhoneNumber = "+998770079639",
            ImagePath = imageFile,
            PasswordHash = "AAaa11@@"
        };
        var validator = new MasterCreateValidator();
        var result = validator.Validate(masterCreateDto);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R", " ", "+999889997730", "1234567")]
    [InlineData("", "STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R ", "999889997730", "12345678")]
    [InlineData(" ", "STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R ", "999 997730", "123  5678")]
    [InlineData("zz", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "123 45 67", "AABBQQ!!")]
    [InlineData("STeeDAyKDgfDcYl      QCBaX5egIxI5R aa", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "-123 45 67", "AABBQQ!!")]
    [InlineData("STeeDAyKDgf12321323sasxxxaX5egIxI5R aa", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "+123451234567", "AABBQQ!!")]

    public void Should1ReturnInValidValidation(string name, string lastName, string phone, string password)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(3 * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        MasterCreateDto masterCreateDto = new MasterCreateDto()
        {
            FirstName = name,
            LastName = lastName,
            PhoneNumber = phone,
            ImagePath = imageFile,
            PasswordHash = password

        };
        var validator = new MasterCreateValidator();
        var result = validator.Validate(masterCreateDto);
        Assert.False(result.IsValid);
    }
}
