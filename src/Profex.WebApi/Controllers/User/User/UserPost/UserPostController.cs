using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Posts;
using Profex.Persistance.Validations.Dtos.Posts;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.PostRequests;
using Profex.Service.Interfaces.Posts;
using System.Security.Principal;

namespace Profex.WebApi.Controllers.User.UserCommon.UserCommonPost
{
    [Route("api/user/post")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly int maxPageSize = 10;
        private readonly IPostService _service;
        private readonly IIdentityService _identity;
        private readonly IPostRequestService _requestService;
     
        public UserPostController(IPostService Postservice,
            IIdentityService identity,
            IPostRequestService requestService)
        {
            _service = Postservice;
            _identity = identity;
            _requestService = requestService;
        }
        
        [HttpGet("all/withrequest")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserAllPostWithRequestAsync([FromQuery] int page = 1)
        => Ok(await _requestService.GetUserAllPostWithRequestAsync(_identity.UserId, new PaginationParams(page, maxPageSize)));

        [HttpGet("one/withRequest/{postId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserPostWithRequestAsync(long postId)
            =>Ok(await _requestService.GetUserPostWithRequestAsync(_identity.UserId, postId));


        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAsync([FromForm] PostCreateDto dto)
        {
            var validator = new PostCreateValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] PostUpdateDto dto)
        {
            var validator = new PostUpdateValidator();
            var validationResult = validator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _service.UpdateAsync(id, dto));
            else return BadRequest(validationResult.Errors);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(id));
    
    }
}
