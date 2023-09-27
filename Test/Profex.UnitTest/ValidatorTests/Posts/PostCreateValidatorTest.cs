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
            CategoryId = 1,
          
            Title = title,
            Price = 1200,
            Description = "QRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG",
            Region = "Navoi",
            District = "Nimadir",
            Latidute = 1123.23,
            Longitude = 12123.23,
            PhoneNumber = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("123.12212211212121212121212121212121212121212")]
    [InlineData("0.122222222222222222222222222222222222222222222222222")]
    [InlineData("12.123123123123123123123123123123213123")]
    [InlineData("123.34234234234234234234234234234234234234234234")]
    [InlineData("-12.1231212")]
    [InlineData("1231231231231231232131221321312321321321213213123123.1")]
    [InlineData("123.123333333333333333333333333333333333333333333333333333333333333333")]
    [InlineData("0.11111111111111111111111111111111111111111111111111111111111111111111111111")]
    [InlineData("12312312312312313123123123123.1")]
    [InlineData("123123123123123.12333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333 ")]
    [InlineData("123.12121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212")]
    [InlineData("123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123.2")]
    [InlineData("1232323232323232323232323232323232323232323.123123123123123123123123123123123123123123")]
    [InlineData("123.123333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333")]
    public void Should1ReturnInValidValidation(string price)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            CategoryId = 1,
       
            Title = "C# program language",
            Price = double.Parse(price),
            Description = "QRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG",
            Region = "Navoi",
            District = "Nimadir",
            Latidute = 2332.123,
            Longitude = 23324.12,
            PhoneNumber = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("kk0f4J96X7kOPfgpxnPLZwtG7KS0EFNKauzzVQzhbEhe5ZA2aCBuArZ5auXUtuosXDzMNGtXvmQESUJGweej8gTWbTgXwEbeAb1kDUroY0S6J4UpfkhBGpKpl73YRLt86Om1oz5in5w8B5c1rQUJxYysPM2EDdMT6jgnVLfJbFiUUxGcZ6MmhyVWXR4oP9UEePOpFx0gvKnnV")]
    [InlineData("    0f4J96X7kOPfgpxnPLZwtG7KS0EFNKauzzVQzhbEhe5ZA2aCBuArZ5auXUtuosXDzMNGtXvmQESUJGweej8gTWbTgXwEbeAb1kDUroY0S6J4UpfkhBGpKpl73YRLt86Om1oz5in5w8B5c1rQUJxYysPM2EDdMT6jgnVLfJbFiUUxGcZ6MmhyVWXR4oP9UEePOpFx0gvKnnV")]
    [InlineData("ytZcidOM3hRrfB0027Vnc2F7qH3SONC3X48mvwmnKMdcNqGVncIS9ZmQtzpjonrxOIyva9iCEfZABT8sjAvdhSrUT8XnuY7c9hSS82QgGkQGtzf3wX32NVuas4jyeYq6UcW6RFNEBVfjnmjHpFPhz2HFO1niLTTkjvV1yV1ltOVvwTvpqsBuKKYwxn3sqilENfUcGOWek")]
    [InlineData("Ml3PY8mczu2zGtFxGCxodBIJ98eZAltYcuPZpdboDzboZ7gQ5eQiiet4gvAceCzZqLMwRyPi7envneKp78z1s6L6vqUcU3HvKeyp2tDaZv5FnbI33Zex648GiUmo5ygmgvf7xyxavOmtkdSgbBe9PeLYs5iEe4Sbw6qlFUxcK07NRxWtnUHKS3HDVxezFRHbkNkLxhsM42")]
    [InlineData("sgH4iRW6pIpqyw8RoAnfYytXrjQcsYVjUWOisoBxpi7l6ngLmCGDHyJax4g3CGaJOfgTw4W69NOcTvhl5zpFucfNf1HRaro4TRbfKFkuQNU5BfUjWT9czuc0IHKbUZ3NrEJA0r5qvjhOy5kSu9C4ItfGJiEn1hbJlAa3nzpybQyntnvMeJ1lMgI1CuRgxHX05BwHeNVBeyt")]
    [InlineData("QRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG")]
 
    public void Should2ReturnInValidValidation(string description)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            CategoryId = 1,
  
            Title = "C# program language",
            Price = 12000.21,
            Description = description,
            Region = "Navoi",
            District = "Nimadir",
            Latidute = 2323.21,
            Longitude = 123.21,
            PhoneNumber = "+998900090909"
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
            CategoryId = 1,
   
            Title = "C# program language",
            Price = 120.200,
            Description = "QRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG",
            Region = Region,
            District = "Nimadir",
            Latidute = 1231.23,
            Longitude = 312.1,
            PhoneNumber = "+998900090909"
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
    [InlineData("122312312312ASD")]
    [InlineData("       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products, we sell an electronic products to our clients, we sell an electronic " +
    "products to our clients")]
    public void Should4ReturnInValidValidation(string District)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            CategoryId = 1,
      
            Title = "C# program language",
            Price = 1200.210,
            Description = "QRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPGQRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG",
            Region = "Region",
            District = District,
            Latidute = 234.12,
            Longitude = 234.23,
            PhoneNumber = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }

    [Theory]
    [InlineData("123.12212211212121212121212121212121212121212")]
    [InlineData("0.122222222222222222222222222222222222222222222222222")]
    [InlineData("12.123123123123123123123123123123213123")]
    [InlineData("123.34234234234234234234234234234234234234234234")]
    [InlineData("-12.1231212")]
    [InlineData("1231231231231231232131221321312321321321213213123123.1")]
    [InlineData("123.123333333333333333333333333333333333333333333333333333333333333333")]
    [InlineData("0.11111111111111111111111111111111111111111111111111111111111111111111111111")]
    [InlineData("12312312312312313123123123123.1")]
    [InlineData("123123123123123.12333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333 ")]
    [InlineData("123.12121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212")]
    [InlineData("123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123.2")]
    [InlineData("1232323232323232323232323232323232323232323.123123123123123123123123123123123123123123")]
    [InlineData("123.123333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333")]
    public void Should5ReturnInValidValidation(string Latidute)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            CategoryId = 1,
       
            Title = "C# program language",
            Price = 233.234,
            Description = "QRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG",
            Region = "Region",
            District = "ADAFDDFDFDF",
            Latidute = Convert.ToDouble(Latidute),
            Longitude = 234.23,
            PhoneNumber = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);


    }
    [Theory]
    [InlineData("123.12212211212121212121212121212121212121212")]
    [InlineData("0.122222222222222222222222222222222222222222222222222")]
    [InlineData("12.123123123123123123123123123123213123")]
    [InlineData("123.34234234234234234234234234234234234234234234")]
    [InlineData("-12.1231212")]
    [InlineData("1231231231231231232131221321312321321321213213123123.1")]
    [InlineData("123.123333333333333333333333333333333333333333333333333333333333333333")]
    [InlineData("0.11111111111111111111111111111111111111111111111111111111111111111111111111")]
    [InlineData("12312312312312313123123123123.1")]
    [InlineData("123123123123123.12333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333 ")]
    [InlineData("123.12121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212121212")]
    [InlineData("123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123.2")]
    [InlineData("1232323232323232323232323232323232323232323.123123123123123123123123123123123123123123")]
    [InlineData("123.123333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333")]
    public void Should6ReturnInValidValidation(string Longitude)
    {
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            CategoryId = 1,

            Title = "C# program language",
            Price = 234.123,
            Description = "QRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG",
            Region = "asdsdfsdf",
            District = "adfsdf",
            Latidute = 12.123,
            //Longitude = Convert.ToDouble(Longitude),
            Longitude = double.Parse(Longitude),
            PhoneNumber = "+998900090909"
        };
        var validator = new PostCreateValidator();
        var res = validator.Validate(postCreateDto);
        Assert.False(res.IsValid);
    }
}
