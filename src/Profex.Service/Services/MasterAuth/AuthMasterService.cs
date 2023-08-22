using Microsoft.Extensions.Caching.Memory;
using Profex.Application.Exceptions;
using Profex.Application.Exceptions.Auth;
using Profex.Application.Exceptions.Masters;
using Profex.Application.Exceptions.Users;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Masters;
using Profex.Domain.Entities.masters;
using Profex.Persistance.Dtos.MasterAuth;
using Profex.Persistance.Dtos.Notifications;
using Profex.Persistance.Dtos.Security;
using Profex.Service.Common.Security;
using Profex.Service.Helpers;
using Profex.Service.Interfaces.Auth;
using Profex.Service.Interfaces.MasterAuth;
using Profex.Service.Interfaces.Notifactions;

namespace Profex.Service.Services.MasterAuth
{
    public class AuthMasterService : IAuthMasterService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMasterRepository _masterRepository;
        private readonly ISmsSender _sender;
        private readonly ITokenService _tokenService;
        private const int CACHED_MINUTES_FOR_REGISTER = 60;
        private const int CACHED_MINUTES_FOR_VERIFICATION = 50;
        private const string REGISTER_CACHE_KEY = "register_";
        private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
        private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
        public AuthMasterService(IMemoryCache memoryCache,
            IMasterRepository masterRepository,
            ISmsSender smsSender,
            ITokenService tokenService)
        {
            this._memoryCache = memoryCache;
            this._masterRepository = masterRepository;
            this._sender = smsSender;
            this._tokenService = tokenService;
        }

        public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
        {
            var master = await _masterRepository.GetByPhoneAsync(loginDto.PhoneNumber);
            if (master is null) throw new MasterNotFoundException();
            var hashResult = PasswordHasher.Verify(loginDto.Password, master.PasswordHash, master.Salt);
            if (hashResult == false) throw new PasswordNotMatchException();
            string token = _tokenService.GenerateToken(master);
            return (Result: true, Token: token);
        }

        public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
        {
            var master = await _masterRepository.GetByPhoneAsync(dto.PhoneNumber);
            if (master is not null) throw new MasterAlreadyExistException(dto.PhoneNumber);
            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterDto cachedRegisterDto))
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

            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
            {
                VerificationDto verificationDto = new VerificationDto();
                verificationDto.Attempt = 0;
                verificationDto.CreatedAt = TimeHelper.GetDateTime();

                //make confirm code as radnom
                verificationDto.Code = CodeGenerator.GenerateRandomNumber();
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
                var smsres = await _sender.SendAsync(smsMessage);
                if (smsres is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
                else return (Result: false, CachedVerificationMinutes: 0);
            }
            else throw new UserCacheDataExpiredException();
        }

        public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
        {

            if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
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
                        if (dbResult)
                        {
                            var master = await _masterRepository.GetByPhoneAsync(phone);
                            string token = _tokenService.GenerateToken(master);
                            return (Result: true, Token: token);
                        }
                        else
                        {
                            throw new NotFoundException();
                        }
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
                else
                {
                    throw new VerificationCodeExpiredException();
                }
            }
            else
            {
                throw new UserCacheDataExpiredException();
            }
        }
        private async Task<bool> RegisterToDatabaseAsync(RegisterDto registerDto)
        {
            var master = new Master();
            master.FirstName = registerDto.FirstName;
            master.LastName = registerDto.LastName;
            master.PhoneNumber = registerDto.PhoneNumber;
            master.PhoneNumberConfirmed = true;
            master.ImagePath = "media/avatarmaster/cats.jpg";
            var haserResult = PasswordHasher.Hash(registerDto.Password);
            master.PasswordHash = haserResult.Hash;
            master.Salt = haserResult.Salt;
            master.CreatedAt = master.UpdatedAt = TimeHelper.GetDateTime();
            var dbResult = await _masterRepository.CreateAsync(master);

            return dbResult > 0;
        }
    }
}
