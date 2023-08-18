using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.Master1;

public class Master1CreateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    //public string PhoneNumberConfirmed { get; set; } = string.Empty;
    public IFormFile ImagePath { get; set; } = default!;
    //public string PasswordHash { get; set; } = string.Empty;
    //public string Salt { get; set; } = string.Empty;
    public bool IsFree { get; set; }
}
