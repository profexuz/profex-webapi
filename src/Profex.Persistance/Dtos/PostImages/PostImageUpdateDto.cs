using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.PostImages
{
    public class PostImageUpdateDto
    {
        public long Post_id { get; set; }
        public IFormFile Image_path { get; set; } = default!;

    }
}
