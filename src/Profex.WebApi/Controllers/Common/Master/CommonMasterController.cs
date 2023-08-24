using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Domain.Entities.master_skills;
using Profex.Persistance.Dtos.Master1;
using Profex.Persistance.Validations.Dtos.Masters;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Master1;

namespace Profex.WebApi.Controllers.Common.Master
{
    [Route("api/common/master")]
    [ApiController]
    public class CommonMasterController : ControllerBase
    {
        private readonly IMaster1Service _service;
        private readonly IPaginator _paginator;
        private readonly int maxPageSize = 30;
        public CommonMasterController(IMaster1Service service, IPaginator paginator)
        {
            this._service = service;
            this._paginator = paginator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
        => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        
        [HttpGet("{masterId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long masterId)
        => Ok(await _service.GetByIdAsync(masterId));

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync([FromQuery] string search, [FromQuery] int page = 1)
            => Ok(await _service.SearchAsync(search, new PaginationParams(page, maxPageSize)));


        [HttpGet("sort/bySkill")]
        public async Task<ActionResult<IList<Master_skill>>> GetPostsByCategory(long skillId)
        {
            var ps = await _service.SortBySkillId(skillId);
            return Ok(ps);
        }

        [HttpDelete("{masterId}")]
        [Authorize(Roles = "Master,Admin")]
        public async Task<IActionResult> DeleteAsync(long masterId)
            => Ok(await _service.DeleteAsync(masterId));

        
        
        [HttpPut("{masterId}")]
        [Authorize(Roles ="Master,Admin")]
        public async Task<IActionResult> UpdateAsync(long masterId, [FromForm] Master1UpdateDto dto)
        {

            var updateValidator = new MasterUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _service.UpdateAsync(masterId, dto));
            else return BadRequest(result.Errors);
        }


    }
}
