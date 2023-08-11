using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.PostImages;

public class PostImageCreateDto 
{
    public long Post_id { get; set; }
    public IFormFile Image_path { get; set; } = default!;
}
