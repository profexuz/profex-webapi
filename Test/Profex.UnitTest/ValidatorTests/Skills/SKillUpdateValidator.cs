using Profex.Persistance.Dtos.Skills;
using Profex.Persistance.Validations.Dtos.Skills;

namespace Profex.UnitTest.ValidatorTests.Skills
{
    public class SKillUpdateValidator
    {
        [Theory]
        [InlineData("", "1234dsfsdfADASD@#!@#!@#adsasdQRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG", 0)]
        [InlineData(" ", "!23aADAdsddsds!@#!@#!@3adsdasdQRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG", -9)]
        [InlineData("  ", "!", -9)]
        [InlineData("3aADAdsddsds!@#!@#!@3adsdasdQRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLt ", "A1", -10)]
        [InlineData("3aADAdsddsds!@#!@#!@3adsdasdQRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLt", "aAd", 0)]
        [InlineData("a", "A0as12312da!@#!@#sdsdfsdfsdfQRzJ9jTqHjYaJ9REoI2o1olXleTG8AJmoMQNt9CaN7KISNHuavKXNnTfLttYlea8VgevdHkvqZXKjqxGu4Oszz8KfPSnd656AZGLOmtotnlT8NbrkQn9aSO00wMUYvZIMaDKB6rp2nQSf7yJ2fHGMz3xbDdCoom40CSnQWosp4vGxVKOjfOQ1HcsSGqToyi8TjxGHmub4u0Vc3IwPG", -9)]
        [InlineData(" ", " ", 0)]
        [InlineData("   ", "  ", -1)]
        public void ShouldReturnInValidValidation(string title, string description, long id)
        {
            SkillCreateDto skillCreateDto = new SkillCreateDto()
            {
                CategoryId = id,
                Description = description,
                Title = title,
            };
            var validator = new SkillCreateValidator();
            var res = validator.Validate(skillCreateDto);
            Assert.False(res.IsValid);
        }
    }
}
