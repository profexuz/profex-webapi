using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.Master1;

public class Master1UpdateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public IFormFile ImagePath { get; set; } = default!;
}
