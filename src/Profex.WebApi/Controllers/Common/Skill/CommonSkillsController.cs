using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Service.Interfaces.Skills;

namespace Profex.WebApi.Controllers.Common.Skill
{
    [Route("api/common/skills")]
    [ApiController]
    public class CommonSkillsController : ControllerBase
    {
        private readonly ISkillService _service;
        private readonly int maxPageSize = 30;

        public CommonSkillsController(ISkillService skillService)
        {
            _service = skillService;
        }

        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{id}")]
        //[AllowAnonymous]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetByIdAsync(id));      
    }
}
