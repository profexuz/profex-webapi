using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.User1;
using Profex.Service.Interfaces.User1;

namespace Profex.WebApi.Controllers.User.User1
{
    [Route("api/[controller]")]
    [ApiController]
    public class User1Controller : ControllerBase
    {
        private readonly IUser1Service _service;
        public User1Controller(IUser1Service service)
        {
            this._service = service;
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
