using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Master1;
using Profex.Persistance.Validations.Dtos.Masters;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.Master1;
using Profex.Service.Services.Identity;
using Profex.WebApi.Configurations;

namespace Profex.WebApi.Controllers.Master
{
    [Route("api/tokenmaster")]
    [ApiController]
    public class TokenMaster : ControllerBase
    {
        private readonly IMaster1Service _masterService;
        private readonly IIdentityService _identity;

        public TokenMaster(IMaster1Service masterService, IIdentityService identity)
        {
            _masterService = masterService;
            this._identity = identity;
        }


        [HttpDelete]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteMasterAsync()
            => Ok(await _masterService.DeleteMasterAsync());


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
    }
}
