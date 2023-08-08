namespace Profex.Domain.Entities.post_images;

public class Post_image : Auditable
{
    public long Post_id { get; set; }

    public string Image_path { get; set; } = string.Empty;
}
