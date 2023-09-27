using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Users;
using Profex.Persistance.Validations.Dtos.Users;
using Profex.Service.Interfaces.Categories;
using Profex.Service.Interfaces.Masters;
using Profex.Service.Interfaces.Posts;
using Profex.Service.Interfaces.Users;

namespace Profex.WebApi.Controllers.Common.User
{
    [Route("api/common/users")]
    [ApiController]
    public class CommonUserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMasterService _msService;
        private readonly IPostService _postService;
        private readonly int maxPageSize = 4;
        public CommonUserController(IUserService service, 
                                IMasterService master1Service, IPostService Postservice)
        {
            this._service = service;
            this._msService = master1Service;
                _postService = Postservice;
        }
 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long userId)
            => Ok(await _service.GetByIdAsync(userId));


        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> CountAsync()
             => Ok(await _service.CountAsync());


        [HttpGet("search/{user}")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchUserAsync(string user, int page = 1)
            => Ok(await _service.SearchUserAsync(user, new PaginationParams(page, maxPageSize)));


        //[HttpGet("posts/{userId}")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetUserAllPostAsync(long userId, int page = 1)
        //=> Ok(await _postService.GetUserAllPostAsync(userId, new PaginationParams(page, maxPageSize)));


        //[HttpGet("getSkillsById")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetMasterSkillById(long masterId)
        //    => Ok(await _msService.GetMasterSkillById(masterId));




    }
}
