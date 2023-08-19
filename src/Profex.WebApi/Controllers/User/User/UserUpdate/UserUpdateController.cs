using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.User1;
using Profex.Service.Interfaces.User1;

namespace Profex.WebApi.Controllers.User.User.UserUpdate
{
    [Route("api/user/update")]
    [ApiController]
    public class UserUpdateController : ControllerBase
    {
        private readonly IUser1Service _service;
        public UserUpdateController(IUser1Service service)
        {
            _service = service;
        }
        [HttpPut("{userId}")]

        public async Task<IActionResult> UpdateAsync(long userId, [FromForm] User1UpateDto dto)
        {

            /*var updateValidator = new CarUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _repository.UpdateAsync(carId, dto));
            else return BadRequest(result.Errors);*/

            return Ok(await _service.UpdateAsync(userId, dto));
        }
    }
}
