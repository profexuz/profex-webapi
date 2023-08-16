using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Posts;
using Profex.Service.Interfaces.Posts;

namespace Profex.WebApi.Controllers.Master.MasterCommon.MasterCommonPost
{
    [Route("api/master/masterPost")]
    [ApiController]
    public class MasterCommonPostController : MasterBaseController
    {
        private readonly int maxPageSize = 30;
        private readonly IPostService _service;

        public MasterCommonPostController(IPostService Postservice)
        {
            _service = Postservice;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{postId}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetByIdAsync(long postId)
        => Ok(await _service.GetByIdAsync(postId));

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] PostCreateDto dto)
        {
            //var validator = new CompanyCreateValidator();
            //var result = validator.Validate(dto);
            //if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            return Ok(await _service.CreateAsync(dto));

            //await _service.CreateAsync(dto);
            //else return BadRequest(result.Errors);
        }

        [HttpPut("(postId)")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long postId, [FromForm] PostUpdateDto dto)
        {
            //var validator = new CompanyUpdateValidator();
            //var validationResult = validator.Validate(dto);
            //if (validationResult.IsValid) return Ok(await _service.UpdateAsync(companyId, dto));
            //else return BadRequest(validationResult.Errors);
            return Ok(await _service.UpdateAsync(postId, dto));
        }

        [HttpDelete("{categoryId}")]
        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long postId)
            => Ok(await _service.DeleteAsync(postId));
    }
}
