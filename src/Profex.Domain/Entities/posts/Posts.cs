namespace Profex.Domain.Entities.posts;

public class Posts : Auditable
{
    public  long  Category_id { get; set; }

    public  long  User_id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Price { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Lattidute { get; set; } = string.Empty;

    public string Longitude { get; set; } = string.Empty;

    public string Phone_number { get; set; } = string.Empty;

}
