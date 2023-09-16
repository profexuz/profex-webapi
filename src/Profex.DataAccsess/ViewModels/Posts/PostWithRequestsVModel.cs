using Profex.Domain.Entities;
using Profex.Domain.Entities.post_images;
using Profex.Domain.Entities.postRequests;

namespace Profex.DataAccsess.ViewModels.Posts;

public class PostWithRequestsVModel : Auditable
{
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public double Longitude { get; set; }
    public double Latidute { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public List<Post_image> Images { get; set; } = new List<Post_image>();
    public List<Request> Request {  get; set; } = new List<Request>();

}
