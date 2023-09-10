using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.User1;
using Profex.Persistance.Validations.Dtos.Users;
using Profex.Service.Interfaces.Master1;
using Profex.Service.Interfaces.User1;

namespace Profex.WebApi.Controllers.Common.User
{
    [Route("api/common/user")]
    [ApiController]
    public class CommonUserController : ControllerBase
    {
        private readonly IUser1Service _service;
        private readonly IMaster1Service _msService;
        private readonly int maxPageSize = 4;
        public CommonUserController(IUser1Service service, IMaster1Service master1Service)
        {
            this._service = service;
            this._msService = master1Service;
        }
 

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> CountAsync()
             => Ok(await _service.CountAsync());

        [HttpGet("getbyId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long userId)
            => Ok(await _service.GetByIdAsync(userId));

        [HttpGet("getSkillsById")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMasterSkillById(long masterId)
            => Ok(await _msService.GetMasterSkillById(masterId));
        


      
    }
}
