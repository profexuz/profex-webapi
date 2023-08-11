using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.Users;

public class UserCreateDto
{
    public string First_name { get; set; } = string.Empty;
    public string Last_name { get; set; } = string.Empty;
    public string Phone_number { get; set; } = string.Empty;
    public bool Phone_number_confirmed { get; set; }
    public IFormFile Image_path { get; set; } = default!;
    public string Password_hash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}
