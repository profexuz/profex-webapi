using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Profex.Persistance.Dtos.Masters;
using Profex.Persistance.Validations.Dtos.Masters;
using System.Text;
using Profex.Persistance.Dtos.Posts;
using Profex.Persistance.Validations.Dtos.Posts;

namespace Profex.UnitTest.ValidatorTests.Posts;

public class PostCreateValidatorTest
{
    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
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
    public void ShouldReturnInValidValidation(string title)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Category_id = 1,
            User_id = 1,
            Title = title,
            Price = 1200,
            Description = "Assalomu alekum hammga c# dasturlash tili zo'r til",
            Region = "Navoi",
            District = "Nimadir",
            Latidute = 1123,
            Longitude = 12123,
            Phone_number = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
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
    [InlineData("A12312312312")]
    [InlineData("a122312312312")]
    [InlineData("122312312312ASD")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
        "products to our clients")]
    public void Should1ReturnInValidValidation(string price)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Category_id = 1,
            User_id = 1,
            Title = "C# program language",
            Price = 312,
            Description = "Assalomu alekum hammga c# dasturlash tili zo'r til",
            Region = "Navoi",
            District = "Nimadir",
            Latidute = 2332,
            Longitude = 23324,
            Phone_number = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
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
    [InlineData("A12312312312")]
    [InlineData("a122312312312")]
    [InlineData("122312312312ASD")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
    "products to our clients")]
    public void Should2ReturnInValidValidation(string description)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Category_id = 1,
            User_id = 1,
            Title = "C# program language",
            Price = 12000,
            Description = description,
            Region = "Navoi",
            District = "Nimadir",
            Latidute = 2323,
            Longitude = 123,
            Phone_number = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
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
    [InlineData("A12312312312")]
    [InlineData("a122312312312")]
    [InlineData("122312312312ASD")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
    "products to our clients")]
    public void Should3ReturnInValidValidation(string Region)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Category_id = 1,
            User_id = 1,
            Title = "C# program language",
            Price = 12000,
            Description = "C# program language is best",
            Region = Region,
            District = "Nimadir",
            Latidute = 123123,
            Longitude = 3121,
            Phone_number = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
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
    [InlineData("A12312312312")]
    [InlineData("a122312312312")]
    [InlineData("122312312312ASD")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
    "products to our clients")]
    public void Should4ReturnInValidValidation(string District)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Category_id = 1,
            User_id = 1,
            Title = "C# program language",
            Price = 12000,
            Description = "C# program language is best",
            Region = "Region",
            District = District,
            Latidute = 234,
            Longitude = 234,
            Phone_number = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
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
    [InlineData("A12312312312")]
    [InlineData("a122312312312")]
    [InlineData("122312312312ASD")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
    "products to our clients")]
    public void Should5ReturnInValidValidation(string Latidute)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Category_id = 1,
            User_id = 1,
            Title = "C# program language",
            Price = 234,
            Description = "C# program language is best",
            Region = "Region",
            District = "ADAFDDFDFDF",
            Latidute = Convert.ToDouble(Latidute),
            Longitude = 234,
            Phone_number = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("1234dsfsdfADASD@#!@#!@#adsasd")]
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
    [InlineData("A12312312312")]
    [InlineData("a122312312312")]
    [InlineData("122312312312ASD")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
    "products to our clients")]
    public void Should6ReturnInValidValidation(string Longitude)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Category_id = 1,
            User_id = 1,
            Title = "C# program language",
            Price = 234,
            Description = "C# program language is best",
            Region = "asdsdfsdf",
            District = "adfsdf",
            Latidute = 12,
            Longitude = Convert.ToDouble(Longitude),
            Phone_number = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }
}
