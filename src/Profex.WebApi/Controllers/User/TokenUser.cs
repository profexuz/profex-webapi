using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.User1;
using Profex.Persistance.Validations.Dtos.Users;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.User1;

namespace Profex.WebApi.Controllers.User
{
    [Route("api/tokenuser")]
    [ApiController]
    public class TokenUser : ControllerBase
    {
        private readonly IUser1Service _service;
        private readonly IIdentityService _identity;

        public TokenUser(IUser1Service service, IIdentityService identity)
        {
            this._service = service;
            this._identity = identity;
        }


        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync( [FromForm] User1UpateDto dto)
        {
            var updateValidator = new UserUpdateValidator();
            var result = updateValidator.Validate(dto);
            long id = _identity.UserId;
            if (result.IsValid) return Ok(await _service.UpdateAsync(id, dto));
            
            else return BadRequest(result.Errors);
        }


        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync()
        {
            long id = _identity.UserId;
            
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
