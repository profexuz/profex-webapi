using Profex.Domain.Entities;

namespace Profex.DataAccsess.ViewModels.Users;

public class User1ViewModel : Auditable
{
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public double Latidute { get; set; }
    public double Longitude { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImagePath { get; set;} = string.Empty;
}
