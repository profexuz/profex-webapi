using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.User1;
using Profex.Persistance.Validations.Dtos.Users;
using Profex.Service.Interfaces.User1;

namespace Profex.WebApi.Controllers.Admin.User
{
    [Route("api/admin/user")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IUser1Service _service;


        public AdminUserController(IUser1Service service)
        {
            this._service = service;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] User1UpateDto dto)
        {

            var updateValidator = new UserUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.UpdateAsync(id, dto));
            else return BadRequest(result.Errors);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
          => Ok(await _service.DeleteAsync(id));
    }
}
