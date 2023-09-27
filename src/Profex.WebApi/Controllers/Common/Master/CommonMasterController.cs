using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.Domain.Entities.master_skills;
using Profex.Persistance.Dtos.Masters;
using Profex.Persistance.Validations.Dtos.Masters;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Masters;
using Profex.Service.Interfaces.MasterSkill;

namespace Profex.WebApi.Controllers.Common.Master
{
    [Route("api/common/masters")]
    [ApiController]
    public class CommonMasterController : ControllerBase
    {
        private readonly IMasterService _service;
        private readonly IPaginator _paginator;
        private readonly IMasterSkillService _skillService;
        private readonly int maxPageSize = 4;
        public CommonMasterController(IMasterService service, IPaginator paginator,
                                        IMasterSkillService skillService)
        {
            this._service = service;
            this._paginator = paginator;
            _skillService = skillService;
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



        //[HttpGet("sort/bySkill")]
        //[AllowAnonymous]
        //public async Task<ActionResult<IList<Master_skill>>> GetPostsByCategory(long skillId)
        //{
        //    var ps = await _service.SortBySkillId(skillId);
        //    return Ok(ps);
        //}

        [HttpGet("skills")]
        [AllowAnonymous]
        public async Task<ActionResult<MasterWithSkillsModel>> GetMasterWithSkillsAsync(long masterId)
        {
            var masterWithSkill = await _service.GetMasterWithSkillsAsync(masterId);
            return Ok(masterWithSkill);
        }



    }
}
