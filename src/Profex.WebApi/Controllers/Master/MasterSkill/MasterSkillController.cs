using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Service.Interfaces.MasterSkill;

namespace Profex.WebApi.Controllers.Master.MasterSkill
{
    [Route("api/master/master-skills")]
    [ApiController]
    public class MasterSkillController : ControllerBase
    {
        private readonly IMasterSkillService _service;
        private readonly int maxPageSize = 30;
        public MasterSkillController(IMasterSkillService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{skillId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long skillId)
            => Ok(await _service.GetByIdAsync(skillId));


        //[HttpPost]
        //[Authorize(Roles = "Master")]
        //public async Task<IActionResult> CreateAsync([FromForm] MasterSkillCreateDto dto)
        //{
        //    var validator = new MasterSkillCreateValidator();
        //    var result = validator.Validate(dto);
        //    if (result.IsValid) return Ok(await _service.CreateAsync(dto));
        //    else return BadRequest(result.Errors);
        //}


        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Master")]
        //public async Task<IActionResult> DeleteAsync(long id)
        //    => Ok(await _service.DeleteAsync(id));
    }
}
