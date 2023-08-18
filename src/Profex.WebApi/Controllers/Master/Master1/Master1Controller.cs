using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Master1;
using Profex.Service.Interfaces.Master1;

namespace Profex.WebApi.Controllers.Master.Master1
{
    [Route("api/[controller]")]
    [ApiController]
    public class Master1Controller : ControllerBase
    {
        private readonly IMaster1Service _service;
        public Master1Controller(IMaster1Service service)
        {
            this._service = service;
        }

        //[HttpPut("{carId}")]
        [HttpPut("{masterId}")]

        public async Task<IActionResult> UpdateAsync(long masterId, [FromForm] Master1UpdateDto dto)
        {
            /*var updateValidator = new CarUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _repository.UpdateAsync(carId, dto));
            else return BadRequest(result.Errors);*/
            return Ok(await _service.UpdateAsync(masterId, dto));
        }
    }
}
