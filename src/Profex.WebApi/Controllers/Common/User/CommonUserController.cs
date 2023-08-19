using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Service.Interfaces.Auth;
using Profex.Service.Interfaces.Users;

namespace Profex.WebApi.Controllers.Common.User
{
    [Route("api/common/user")]
    [ApiController]
    public class CommonUserController : CommonBaseController
    {
        private readonly int maxPageSize = 30;
        private readonly IUserService _service;
        private readonly IAuthService _authService;
        public CommonUserController(IUserService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));


        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long userId)
        => Ok(await _service.GetByIdAsync(userId));
    }
}
