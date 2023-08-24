using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.AdminAuth;
using Profex.Persistance.Dtos.Auth;
using Profex.Persistance.Validations.Dtos;
using Profex.Persistance.Validations.Dtos.Admin;
using Profex.Service.Interfaces.AdminAuth;
using Profex.Service.Interfaces.Users;

namespace Profex.WebApi.Controllers.Admin.AdminAuth
{
    [Route("api/administrator")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly int maxPageSize = 30;
        private readonly IUserService _service;
        private readonly IAuthAdminService _authService;
        public AdminAuthController(IUserService service, IAuthAdminService authAdminService)
        {
            this._authService = authAdminService;
            this._service = service;
        }

        [HttpPost("register")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterAdminDto registerDto)
        {
            var validator = new RegisterValdiator();
            var result = validator.Validate(registerDto);
            if (result.IsValid)
            {
                var serviceResult = await _authService.RegisterAsync(registerDto);

                return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
            }
            else return BadRequest(result.Errors);
        }


        [HttpPost("register/send-code")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendCodeRegisterAsync(string phone)
        {
            var result = PhoneNumberValidator.IsValid(phone);
            if (result == false) return BadRequest("Phone number is invalid!");

            var serviceResult = await _authService.SendCodeForRegisterAsync(phone);
            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }


        [HttpPost("register/verify")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _authService.VerifyRegisterAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AdminDto loginDto)
        {
            var validator = new LoginValidator();
            var valResult = validator.Validate(loginDto);
            if (valResult.IsValid == false) return BadRequest(valResult.Errors);
            var serviceResult = await _authService.LoginAsync(loginDto);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }
    }

}
