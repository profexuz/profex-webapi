﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Application.Utils;
using Profex.Persistance.Dtos.Skills;
using Profex.Service.Interfaces.Skills;

namespace Profex.WebApi.Controllers.Master.MasterCommon.MasterCommonSkill
{
    [Route("api/master/")]
    [ApiController]
    public class SkillController : MasterBaseController
    {
        private readonly ISkillService _service;
        private readonly int maxPageSize = 30;

        public SkillController(ISkillService skillService)
        {
            this._service = skillService;
        }

        [HttpGet("skill")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("skill/{id}")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost("skill")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync([FromForm] SkillCreateDto dto)
        {
            //var validator = new CompanyCreateValidator();
            //var result = validator.Validate(dto);
            //if (result.IsValid) return Ok(await _service.CreateAsync(dto));
            return Ok(await _service.CreateAsync(dto));

            //await _service.CreateAsync(dto);
            //else return BadRequest(result.Errors);
        }

        [HttpPut("skill/(id)")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] SkillUpdateDto dto)
        {
            //var validator = new CompanyUpdateValidator();
            //var validationResult = validator.Validate(dto);
            //if (validationResult.IsValid) return Ok(await _service.UpdateAsync(companyId, dto));
            //else return BadRequest(validationResult.Errors);

            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("skill/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _service.DeleteAsync(id));
    }
}
