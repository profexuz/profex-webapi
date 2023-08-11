using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Profex.Persistance.Dtos.PostImages;
using Profex.Persistance.Validations.Dtos.PostImages;
using System.Text;

namespace Profex.UnitTest.ValidatorTests.PostImages
{
    public class PostImageValidatorTest
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
            PostImageCreateDto postImageCreateDto = new PostImageCreateDto()
            {
                Post_id = 1,
                Image_path = imageFile
            };
            var validator = new PostImageValidator();
            var result = validator.Validate(postImageCreateDto);
            Assert.False(result.IsValid);
        }
    }
}
