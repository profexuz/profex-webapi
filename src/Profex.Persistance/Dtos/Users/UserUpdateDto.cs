using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool PhoneNumberConfirmed { get; set; }
    public IFormFile ImagePath { get; set; } = default!;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}
