using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Domain.Entities.master_skills;
using Profex.Domain.Entities.skills;
using Profex.Service.Interfaces.Categories;
using Profex.Service.Interfaces.Masters;
using Profex.Service.Interfaces.Skills;

namespace Profex.WebApi.Controllers.Common.Skill
{
    [Route("api/common/skills")]
    [ApiController]
    public class CommonSkillController : ControllerBase
    {
        private readonly ISkillService _service;
        private readonly int maxPageSize = 30;
        private readonly ICategoryService _categoryservice;
        private readonly IMasterService _masterService;

        public CommonSkillController(ISkillService skillService,
                            ICategoryService Categoryservice,
                            IMasterService masterService)
        {
            _service = skillService;
            _categoryservice = Categoryservice;
            _masterService = masterService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


        [HttpGet("{skillId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long skillId)
            => Ok(await _service.GetByIdAsync(skillId));


        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());


        [HttpGet("categories/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSkillByCategoryId(long categoryId, int page = 1)
        {
            var ps = await _categoryservice.GetAllSkillByCategoryId(categoryId, new PaginationParams(page, maxPageSize));

            return Ok(ps);
        }


        //[HttpGet("sort/masters/{skillId}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<IList<Master_skill>>> GetPostsByCategory(long skillId)
        //{
        //    var ps = await _masterService.SortBySkillId(skillId);
        //    return Ok(ps);
        //}
    }
}
