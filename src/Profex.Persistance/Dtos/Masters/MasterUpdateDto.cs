using Microsoft.AspNetCore.Http;

namespace Profex.Persistance.Dtos.Masters;

public class MasterUpdateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public IFormFile? ImagePath { get; set; }
    public bool IsFree { get; set; } = true;
}
