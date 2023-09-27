using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Users;
using Profex.Persistance.Validations.Dtos.Users;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.Users;

namespace Profex.WebApi.Controllers.User
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IIdentityService _identity;

        public UserController(IUserService service, IIdentityService identity)
        {
            this._service = service;
            this._identity = identity;
        }


        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto dto)
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
