using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Profex.Persistance.Dtos.Masters;
using Profex.Persistance.Validations.Dtos.Masters;
using System.Text;

namespace Profex.UnitTest.ValidatorTests.Masters;

public class MasterCreateValidatorTest
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
    [InlineData("STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R", " ", "+999889997730", "1234567")]
    [InlineData("", "STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R ", "999889997730", "12345678")]
    [InlineData(" ", "STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R ", "999 997730", "123  5678")]
    [InlineData("zz", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "123 45 67", "AABBQQ!!")]
    [InlineData("STeeDAyKDgfDcYl      QCBaX5egIxI5R aa", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "-123 45 67", "AABBQQ!!")]
    [InlineData("STeeDAyKDgf12321323sasxxxaX5egIxI5R aa", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "+123451234567", "AABBQQ!!")]



    public void ShouldReturnInValidValidation(string name, string lastName, string phone, string password)
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
