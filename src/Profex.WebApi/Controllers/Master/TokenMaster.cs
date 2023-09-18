using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Master1;
using Profex.Persistance.Dtos.MasterSkill;
using Profex.Persistance.Dtos.PostRequest;
using Profex.Persistance.Validations.Dtos.Masters;
using Profex.Persistance.Validations.Dtos.MasterSkill;
using Profex.Persistance.Validations.Dtos.PostRequest;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.Master1;
using Profex.Service.Interfaces.MasterSkill;
using Profex.Service.Interfaces.PostRequests;

namespace Profex.WebApi.Controllers.Master
{
    [Route("api/tokenmaster")]
    [ApiController]
    public class TokenMaster : ControllerBase
    {
        private readonly IMaster1Service _masterService;
        private readonly IIdentityService _identity;
        private readonly IMasterSkillService _masterSkill;
        private readonly IPostRequestService _requestService;
        private readonly int maxPageSize = 30;

        public TokenMaster(IMaster1Service masterService,
                IIdentityService identity, IMasterSkillService masterSkill,
                IPostRequestService requestService)
        {
            this._masterService = masterService;
            this._identity = identity;
            this._masterSkill = masterSkill;
            _requestService = requestService;
        }


        [HttpPut]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> UpdateAsync([FromForm] Master1UpdateDto dto)
        {
            long id = _identity.UserId;
            var updateValidator = new MasterUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _masterService.UpdateAsync(id, dto));

            else return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteMasterAsync()
          => Ok(await _masterService.DeleteMasterAsync());


        [HttpPost("addSkill")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> CreateAsync([FromForm] MasterSkillCreateDto dto)
        {
            var validator = new MasterSkillCreateValidator();
            var result = validator.Validate(dto);
            if (result.IsValid) return Ok(await _masterSkill.CreateAsync(dto));
            else return BadRequest(result.Errors);
        }


        [HttpDelete("dSkill/{id}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _masterSkill.DeleteAsync(id));


        [HttpPost("request/post")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> CreateAsync([FromForm] RequestDto dto)
        {
            long masterId = _identity.UserId;
            var validator = new RequestValidator();
            var result = validator.Validate(dto);

            if(result.IsValid) return Ok(await _requestService.RequestToPostAsync(masterId, dto));
            else return BadRequest(result.Errors);
        }


        [HttpGet("posts/requested")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetMasterRequestedAllPostsAsync([FromQuery] int page = 1)
        {
            long masterId = _identity.UserId;

            return Ok(await _requestService.GetMasterRequestedAllPostsAsync(masterId, new PaginationParams(page, maxPageSize)));
        }


        [HttpDelete("post/request")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteRequestAsync([FromForm] RequestDto dto)
        {
            long masterId = _identity.UserId;
            var validator = new RequestValidator();
            var result = validator.Validate(dto);
           
            if(result.IsValid) return Ok( await _requestService.DeleteRequestAsync(masterId, dto.PostId, dto.UserId));
            else return BadRequest(result.Errors);
        }
    }
}
