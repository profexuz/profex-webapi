using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.PostImages;

public class PostImageCreateDto
{
    public long PostId { get; set; }
    public IFormFile ImagePath { get; set; } = default!;
}
