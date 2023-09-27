using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.PostRequest;
using Profex.Persistance.Dtos.Posts;
using Profex.Persistance.Validations.Dtos.PostRequest;
using Profex.Persistance.Validations.Dtos.Posts;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.PostRequests;
using Profex.Service.Interfaces.Posts;

namespace Profex.WebApi.Controllers.User.UserCommon.UserCommonPost
{
    [Route("api/user/posts")]
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
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAllPostAsync(int page = 1)
            => Ok(await _service.GetUserAllPostAsync(_identity.UserId, new PaginationParams(page, maxPageSize)));

        [HttpGet("requests")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserAllPostWithRequestAsync([FromQuery] int page = 1)
        => Ok(await _requestService.GetUserAllPostWithRequestAsync(_identity.UserId, new PaginationParams(page, maxPageSize)));

        [HttpGet("requests/{postId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserPostWithRequestAsync(long postId)
            => Ok(await _requestService.GetUserPostWithRequestAsync(_identity.UserId, postId));


        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAsync([FromForm] PostCreateDto dto)
        {
            var validator = new PostCreateValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }


        [HttpPost("requests")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AcceptRequestAsync([FromForm] RequestAcceptDto dto)
        {
            var validator = new RequestAcceptValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _requestService.AcceptRequestAsync(_identity.UserId, dto));
            else return BadRequest(result.Errors);
        }

        [HttpDelete("requests")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteRequestAsync([FromForm] RequestAcceptDto dto)
        {
            var validator = new RequestAcceptValidator();
            var result = validator.Validate(dto);
            long userId = _identity.UserId;
            if (result.IsValid) return Ok(await _requestService.DeleteRequestAsync(dto.masterId, dto.postId, userId));
            else return BadRequest(result.Errors);
        }

        [HttpPut("{postId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync(long postId, [FromForm] PostUpdateDto dto)
        {
            var validator = new PostUpdateValidator();
            var validationResult = validator.Validate(dto);
            if (validationResult.IsValid) return Ok(await _service.UpdateAsync(postId, dto));
            else return BadRequest(validationResult.Errors);
        }


        [HttpDelete("{postId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync(long postId)
            => Ok(await _service.DeleteAsync(postId));

    }
}
