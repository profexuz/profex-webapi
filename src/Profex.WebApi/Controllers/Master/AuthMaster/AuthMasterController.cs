using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.MasterAuth;
using Profex.Persistance.Validations.Dtos;
using Profex.Persistance.Validations.Dtos.MasterAuth;
using Profex.Service.Interfaces.MasterAuth;

namespace Profex.WebApi.Controllers.Master.MasterAuth
{
    [Route("api/master")]
    [ApiController]
    public class AuthMasterController : ControllerBase
    {
        private readonly IAuthMasterService _authMasterService;
        public AuthMasterController(IAuthMasterService authMasterService)
        {
            _authMasterService = authMasterService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
        {
            var validator = new RegisterValidator();
            var result = validator.Validate(registerDto);
            if (result.IsValid)
            {
                var serviceResult = await _authMasterService.RegisterAsync(registerDto);

                return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
            }
            else return BadRequest(result.Errors);
        }

        [HttpPost("register/send-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCodeRegisterAsync(string phone)
        {
            var result = PhoneNumberValidator.IsValid(phone);
            if (result == false) return BadRequest("Phone number is invalid!");

            var serviceResult = await _authMasterService.SendCodeForRegisterAsync(phone);
            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }

        [HttpPost("register/verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyRegisterAsync([FromForm] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _authMasterService.VerifyRegisterAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        [HttpPost("register/login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDto loginDto)
        {
            var validator = new LoginValidator();
            var valResult = validator.Validate(loginDto);
            if (valResult.IsValid == false) return BadRequest(valResult.Errors);
            var serviceResult = await _authMasterService.LoginAsync(loginDto);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }
    }
}
