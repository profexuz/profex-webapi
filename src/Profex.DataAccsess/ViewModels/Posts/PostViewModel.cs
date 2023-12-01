using Profex.Domain.Entities;
using Profex.Domain.Entities.post_images;

namespace Profex.DataAccsess.ViewModels.Posts
{
    public class PostViewModel : Auditable
    {
        public long CategoryId { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string[] ImagePath { get; set; } = {};
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string CategoryName { get; set;} = string.Empty;
        public List<Post_image> Images { get; set; } = new List<Post_image>();
        public bool Status { get; set; }

        public string Address { get; set; } = String.Empty;

        public string JobTime { get; set; } = String.Empty; 

    }
}
