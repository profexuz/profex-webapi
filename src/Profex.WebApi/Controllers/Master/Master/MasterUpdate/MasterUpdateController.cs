using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Master1;
using Profex.Persistance.Validations.Dtos.Masters;
using Profex.Service.Interfaces.Master1;

namespace Profex.WebApi.Controllers.Master.Master.MasterUpdate
{
    [Route("api/master/update")]
    [ApiController]
    public class MasterUpdateController : ControllerBase
    {
        private readonly IMaster1Service _service;
        public MasterUpdateController(IMaster1Service service)
        {
            _service = service;
        }

        [HttpPut("{masterId}")]

        public async Task<IActionResult> UpdateAsync(long masterId, [FromForm] Master1UpdateDto dto)
        {

            var updateValidator = new MasterUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.UpdateAsync(masterId, dto));
            else return BadRequest(result.Errors);
        }
    }
}
