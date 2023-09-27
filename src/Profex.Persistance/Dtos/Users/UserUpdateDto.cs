using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public IFormFile? ImagePath { get; set; } = null;
}
