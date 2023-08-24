using Profex.Persistance.Dtos.Skills;
using Profex.Persistance.Validations.Dtos.Skills;

namespace Profex.UnitTest.ValidatorTests.Skills
{
    public class SKillUpdateValidator
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
        [InlineData("  asd     ASD123!@#asdas!@aadsfASDA")]
        [InlineData("    ads  asd ASD123!asd@#!@aadsfASDA")]
        [InlineData(" asd      ASD123!@#!@aadsfASDA")]
        [InlineData("  asd     ASD123!@#!@aadsfASDA")]
        [InlineData("ASD123!@#!@aadsfASDA      asd ")]
        [InlineData("ASD123!@#!@aadsfASDsdfsA   assdasdas")]
        [InlineData("   sdf    ASD123!@#!@aadsfASDA      asda12asdsdasd")]
        [InlineData("asd  sdf   sdf  Aasd0!@#!@aadsfASDA   sdf    asasdasd ")]
        [InlineData("electronifsdprodusdfsdfcss, we sfsdell ansdfsd electronic products to our clients, we sell an electronic " +
        "products to our clients")]
        public void ShouldReturnInValidValidation(string description)
        {
            SkillCreateDto skillCreateDto = new SkillCreateDto()
            {
                CategoryId = 1,
                Description = description,
                Title = "Title",
            };
            var validator = new SkillCreateValidator();
            var res = validator.Validate(skillCreateDto);
            Assert.False(res.IsValid);
        }
    }
}
