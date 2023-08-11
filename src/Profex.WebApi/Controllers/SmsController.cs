using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Notifications;
using Profex.Persistance.Interfaces.Auth;
using Profex.Persistance.Interfaces.Notifications;

namespace Profex.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SmsController : ControllerBase
{
    private readonly ISmsSender _smsSender;
    private readonly IIdentityService _identity;

    public SmsController(ISmsSender smsSender, IIdentityService identityService)
    {
        this._smsSender = smsSender;
        this._identity = identityService;
    }

    [HttpPost]
    public async Task<IActionResult> SendAsync([FromBody] SmsMessage smsMessage)
    {
        return Ok(await _smsSender.SendAsync(smsMessage));
    }

    /*[HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(new
        {
            _identity.UserId,
            _identity.FirstName,
            _identity.LastName,
            _identity.PhoneNumber,
            _identity.IdentityRole
        }
        );
    }*/
}
