using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.User1;
using Profex.Persistance.Validations.Dtos.Users;
using Profex.Service.Interfaces.User1;

namespace Profex.WebApi.Controllers.User.User.UserUpdate
{
    [Route("api/user")]
    [ApiController]
    public class UserUpdateController : ControllerBase
    {
        private readonly IUser1Service _service;
        private readonly int maxPageSize = 30;
        public UserUpdateController(IUser1Service service)
        {
            _service = service;
        }
        [HttpPut("update/{userId}")]

        public async Task<IActionResult> UpdateAsync(long userId, [FromForm] User1UpateDto dto)
        {

            var updateValidator = new UserUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.UpdateAsync(userId, dto));
            else return BadRequest(result.Errors);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
             => Ok(await _service.CountAsync());

        [HttpGet("getbyid/{userId}")]
        public async Task<IActionResult> GetByIdAsync(long userId)
            => Ok(await _service.GetByIdAsync(userId));

    }
}
