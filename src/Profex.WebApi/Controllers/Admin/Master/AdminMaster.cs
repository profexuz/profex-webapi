using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Master1;
using Profex.Persistance.Validations.Dtos.Masters;
using Profex.Service.Interfaces.Master1;
using Profex.Service.Services.Identity;

namespace Profex.WebApi.Controllers.Admin.Master
{
    [Route("api/adminMaster")]
    [ApiController]
    public class AdminMaster : ControllerBase
    {
        private readonly IMaster1Service _masterService;
        private readonly IdentityService _identity;

        public AdminMaster(IMaster1Service master1Service )
        {
            _masterService = master1Service;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _masterService.DeleteAsync(id));



        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] Master1UpdateDto dto)
        {
            var updateValidator = new MasterUpdateValidator();
            var result = updateValidator.Validate(dto);
            if (result.IsValid) return Ok(await _masterService.UpdateAsync(id, dto));

            else return BadRequest(result.Errors);
        }

    }
}
