﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profex.Persistance.Dtos.Auth;
using Profex.Persistance.Interfaces.Auth;
using Profex.Persistance.Validations.Dtos;
using Profex.Persistance.Validations.Dtos.Auth;

namespace Profex.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
        {
            var validator = new RegisterValidator();
            var result = validator.Validate(registerDto);
            if (result.IsValid)
            {
                var serviceResult = await _authService.RegisterAsync(registerDto);
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

            var serviceResult = await _authService.SendCodeForRegisterAsync(phone);
            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }
    }

}