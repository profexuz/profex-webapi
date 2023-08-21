using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.PostImages
{
    public class PostImageUpdateDto
    {
        public long PostId { get; set; }
        public IFormFile ImagePath { get; set; } = default!;

    }
}
