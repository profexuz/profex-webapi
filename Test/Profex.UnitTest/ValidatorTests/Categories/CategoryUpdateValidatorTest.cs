using Profex.Persistance.Dtos.Categories;
using Profex.Persistance.Validations.Dtos.Categories;

namespace Profex.UnitTest.ValidatorTests.Categories;

public class CategoryUpdateValidatorTest
{
    [Theory]
    [InlineData("tutyetgjkzocvfmyparyiyxtzfqqszikjkauctqljj",
        "PFoz26dqJHpb8xQkwzuedjl9jyEQoJQ8n6YLnEQzfCyxKGKaskz7qQQTnofr34CutUx6mEivZXBYSijlg4" +
        "ovTaySf5WyrIbv2SRYBsPKiPG8adSWqjLmhlwJEeuEEngjKpHBvj3eVFkCnaFsZ6p9MZQGLyxyRpmkTRmQ jFUFF8" +
        "5WWpDESSLnLN3eKfrnPPouxiObo5s5lEXx5j")]

    [InlineData("8KTABinBLStcWROshqcbu1ypnXi1uWBuscmycHysxIGoVTq3LCH", "B2ADUhSHAJYZmRxYi1jFOWAbSn" +
        "iLDSDKR2ZPq8gL2U2VWmZJQFGpbunEcr52z39KgikhyyED7GcxJhnmYZ9pyqr6gNAtOSZi826FhM5TjZ75CZUFUVE2Xsi8Jj" +
        "RJgDEMSIYOyaV3wtReV0y5J9tc5PmMwG851ffipRGiSruHf7Xr8kVpg2r5QeEPE1sOy5BiJhTxdPCd6C")]

    [InlineData("            aO17A7K003IQvm6TwfKsHczNvTujJ2R1B8rKC", "nssq6mMMONW6x42w1aEkfCd6K8SYkq9fBexibs0a9k3nNkhKHoMmI6GqBykVm486Pbibmk0HzIcA0KHHekh8L6S2fbmwQeC3Rd0KvhOd5AGGACc7Fd3mrTnqdnmrxxGCfa1AouOtLzyjWAV43c6tl7OO1tl963nJL4uAJgZEcUnGDl9kP8hAjnqvZYveuErOjsMncrN0TL")]
    [InlineData("A1!@#!#qwesd@#QEsdfsdfsdf", "")]
    [InlineData("aAdaE!@qeqWE!@#!@#adsadssa1", "12")]
    [InlineData("A0as12312da!@#!@#sdsdfsdfsdf", "a")]
    [InlineData("       dfasdfsdfsdfa12", "")]
    [InlineData("       2323411!@#!@aadsfASDA", "xx")]
    [InlineData("       ASD123!@#!@aadsfASDA", "aa")]
    [InlineData("", "ASD123!@#!@aadsfASDA      asd ")]
    [InlineData("aa", "ASD123!@#!@aadsfASDsdfsA   assdasdas")]
    [InlineData("zx", "       ASD123!@#!@aadsfASDA      asda12asdsdasd")]
    [InlineData("   ", "asd       Aasd0!@#!@aadsfASDA       asasdasd ")]
    [InlineData("electronic products,  we sell an electronic products to our clients", " ")]
    public void ShouldReturnInValidValidation(string name, string description)
    {
        CategoryUpdateDto categoryCreateDto = new CategoryUpdateDto()
        {
            Name = name,
            Description = description
        };

        var validator = new CategoryUpdateValidator();
        var result = validator.Validate(categoryCreateDto);
        Assert.False(result.IsValid);
    }
}
