using Microsoft.AspNetCore.Mvc;
using Profex.Service.Interfaces.Identity;

[Route("api/identity")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identity;

    public IdentityController(IIdentityService identity)
    {
        _identity = identity;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(new
        {
            _identity.FirstName,
            _identity.LastName,
            _identity.PhoneNumber,
            _identity.UserId,
            _identity.IdentityRole
        }
        );
    }
}
