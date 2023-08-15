namespace Profex.Persistance.Dtos.Posts;

public class PostCreateDto
{
    public long Category_id { get; set; }
    public long User_id { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public double Latidute { get; set; }
    public double Longitude { get; set; } 
    public string Phone_number { get; set; } = string.Empty;
}
