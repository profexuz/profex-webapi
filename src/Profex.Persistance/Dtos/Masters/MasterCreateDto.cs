using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.Masters;

public class MasterCreateDto
{
    public string FirstName { get; set; } =String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Phone_number { get; set; } = string.Empty;
    public string Phone_number_confirmed { get; set; } = string.Empty;
    public IFormFile Image_path { get; set; } = default!;
    public string Password_hash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public bool Is_free { get; set; }
}
