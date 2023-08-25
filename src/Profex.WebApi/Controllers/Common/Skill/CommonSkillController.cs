using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Skills;
using Profex.Persistance.Validations.Dtos.Skills;
using Profex.Service.Interfaces.Skills;

namespace Profex.WebApi.Controllers.Common.Skill
{
    [Route("api/common/skills")]
    [ApiController]
    public class CommonSkillController : ControllerBase
    {
        private readonly ISkillService _service;
        private readonly int maxPageSize = 30;

        public CommonSkillController(ISkillService skillService)
        {
            _service = skillService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetByIdAsync(id));


        [HttpPost("skill")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] SkillCreateDto dto)
        {
            var validator = new SkillCreateValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }

        
        [HttpPut("skill/(id)")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] SkillUpdateDto dto)
        {
            //var validator = new SkillCreateValidator();
            
            //var result = validator.Validate(id,dto)
            ////var validationResult = validator.Validate(dt);
            //if (result.IsValid) return Ok(await _service.UpdateAsync(id, dto));
            //else return BadRequest(validationResult.Errors);
            return Ok(await _service.UpdateAsync(id, dto));
        }


        [HttpDelete("skill/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(id));
    }
}
