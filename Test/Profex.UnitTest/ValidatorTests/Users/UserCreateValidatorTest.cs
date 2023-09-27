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
    [InlineData("STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R", " ", "+999889997730", "1234567")]
    [InlineData("", "STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R ", "999889997730", "12345678")]
    [InlineData(" ", "STeeDAyKDgfDcYlbeTdo5QCBaX5egIxI5R ", "999 997730", "123  5678")]
    [InlineData("zz", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "123 45 67", "AABBQQ!!")]
    [InlineData("STeeDAyKDgfDcYl      QCBaX5egIxI5R aa", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "-123 45 67", "AABBQQ!!")]
    [InlineData("STeeDAyKDgf12321323sasxxxaX5egIxI5R aa", "STeeDAyKDgfDcYl      QCBaX5egIxI5R ", "+123451234567", "AABBQQ!!")]

    public void ShouldReturnInValidValidation(string name, string lastName, string phone, string password)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(4 * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", "file.png");
        UserCreateDto userCreateDto = new UserCreateDto()
        {
            FirstName = name,
            LastName = lastName,
            PhoneNumber = phone,
            PhoneNumberConfirmed = true,
            ImagePath = imageFile,
            PasswordHash = password
        };
        var validator = new UserCreateValidator();
        var result = validator.Validate(userCreateDto);
        Assert.False(result.IsValid);
    }
      
}
