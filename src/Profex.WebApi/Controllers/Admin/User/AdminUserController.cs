using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Users;
using Profex.Persistance.Validations.Dtos.Users;
using Profex.Service.Interfaces.Users;

namespace Profex.WebApi.Controllers.Admin.User
{
    [Route("api/admin/users")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserService _service;


        public AdminUserController(IUserService service)
        {
            this._service = service;
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long userId, [FromForm] UserUpdateDto dto)
        {

            var updateValidator = new UserUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.UpdateAsync(userId, dto));
            else return BadRequest(result.Errors);
        }


        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long userId)
          => Ok(await _service.DeleteAsync(userId));
    }
}
