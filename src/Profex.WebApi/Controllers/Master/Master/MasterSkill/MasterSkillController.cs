using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.MasterSkill;
using Profex.Persistance.Validations.Dtos.MasterSkill;
using Profex.Service.Interfaces.MasterSkill;

namespace Profex.WebApi.Controllers.Master.Master.MasterSkill
{
    [Route("api/master/masterskill")]
    [ApiController]
    public class MasterSkillController : ControllerBase
    {
        private readonly IMasterSkillService _service;
        private readonly int maxPageSize = 30;
        public MasterSkillController(IMasterSkillService service)
        {
            this._service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetByIdAsync(id));


        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> CreateAsync([FromForm] MasterSkillCreateDto dto)
        {
            var validator = new MasterSkillCreateValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }

  

        [HttpDelete("{id}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(id));
    }
}
