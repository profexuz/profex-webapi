﻿using Microsoft.Extensions.Caching.Memory;
using Profex.Application.Exceptions.Auth;
using Profex.Application.Exceptions.Users;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Admins;
using Profex.DataAccsess.Interfaces.Users;
using Profex.Domain.Entities.admins;
using Profex.Domain.Entities.users;
using Profex.Persistance.Dtos.AdminAuth;
using Profex.Persistance.Dtos.Notifications;
using Profex.Persistance.Dtos.Security;
using Profex.Service.Common.Security;
using Profex.Service.Helpers;
using Profex.Service.Interfaces.AdminAuth;
using Profex.Service.Interfaces.Notifactions;

namespace Profex.Service.Services.AdminAuth
{
    public class AuthAdminService : IAuthAdminService
    {
        private readonly IMemoryCache _memoryCache;

        private readonly ISmsSender _smsSender;
        private readonly ITokenAdminService _tokenService;
        private readonly IAdminsRepository _adminRepository;
        private const int CACHED_MINUTES_FOR_REGISTER = 60;
        private const int CACHED_MINUTES_FOR_VERIFICATION = 50;
        private const string REGISTER_CACHE_KEY = "register_";
        private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
        private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

        public AuthAdminService(IMemoryCache memoryCache,
         
            ISmsSender smsSender, IAdminsRepository adminsRepository,
            ITokenAdminService tokenAdminService)
        {
            this._memoryCache= memoryCache;
          
            this._smsSender= smsSender;
            this._tokenService= tokenAdminService;
            this._adminRepository = adminsRepository;
        }

        public async Task<(bool Result, string Token)> LoginAsync(AdminDto loginDto)
        {
            var admin = await _adminRepository.GetByPhoneAsync(loginDto.PhoneNumber);
            if (admin is null) throw new UserNotFoundException();

            var hasherResult = PasswordHasher.Verify(loginDto.Password, admin.PasswordHash, admin.Salt);
            if (hasherResult == false) throw new PasswordNotMatchException();

            string token = _tokenService.GenerateToken(admin);
            return (Result: true, Token: token);
        }

        public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterAdminDto dto)
        {
            var user = await _adminRepository.GetByPhoneAsync(dto.PhoneNumber);
            if (user is not null) throw new UserAlreadyExistException(dto.PhoneNumber);

            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterAdminDto cachedRegisterDto))
            {
                cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
                _memoryCache.Remove(REGISTER_CACHE_KEY + dto.PhoneNumber);
            }
            else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

            return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
        }

        public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
        {
            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterAdminDto registerDto))
            {
                VerificationDto verificationDto = new VerificationDto();
                verificationDto.Attempt = 0;
                verificationDto.CreatedAt = TimeHelper.GetDateTime();

                // make confirm code as random
                //verificationDto.Code = CodeGenerator.GenerateRandomNumber();
                verificationDto.Code = 1234;

                if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                }

                _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                    TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

                SmsMessage smsMessage = new SmsMessage();
                smsMessage.Title = "ProFex";
                smsMessage.Content = "Your verification code : " + verificationDto.Code;
                smsMessage.Recipent = phone.Substring(1);

                var smsResult = true;   //await _smsSender.SendAsync(smsMessage);
                if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
                else return (Result: false, CachedVerificationMinutes: 0);
            }
            else throw new UserCacheDataExpiredException();
        }

        public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
        {
            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterAdminDto registerDto))
            {
                if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
                {
                    if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    {
                        throw new VerificationTooManyRequestsException();
                    }
                    else if (verificationDto.Code == code)
                    {
                        var dbResult = await RegisterToDatabaseAsync(registerDto);
                        if (dbResult is true)
                        {
                            var user = await _adminRepository.GetByPhoneAsync(phone);
                            string token = _tokenService.GenerateToken(user);
                            return (Result: true, Token: token);
                        }
                        else return (Result: false, Token: "");
                    }
                    else
                    {
                        _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                        verificationDto.Attempt++;
                        _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                        return (Result: false, Token: "");
                    }
                }
                else throw new VerificationCodeExpiredException();
            }
            else throw new UserCacheDataExpiredException();
        }

        private async Task<bool> RegisterToDatabaseAsync(RegisterAdminDto? registerDto)
        {
            var user = new Admin();
            user.FirstName = registerDto.FirstName;
            user.LastName = registerDto.LastName;
            user.PhoneNumber = registerDto.PhoneNumber;
         
            var haserResult = PasswordHasher.Hash(registerDto.Password);
            user.PasswordHash = haserResult.Hash;
            user.Salt = haserResult.Salt;
            user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();
            var dbResult = await _adminRepository.CreateAsync(user);

            return dbResult > 0;
        }
    }
}
