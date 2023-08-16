namespace Profex.Persistance.Dtos.Posts;

public class PostUpdateDto
{
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Lattidute { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
