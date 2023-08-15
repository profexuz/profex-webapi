using Microsoft.Extensions.Caching.Memory;
using Profex.Application.Exceptions.Auth;
using Profex.Application.Exceptions.Users;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Masters;
using Profex.Domain.Entities.masters;
using Profex.Persistance.Dtos.MasterAuth;
using Profex.Persistance.Dtos.Notifications;
using Profex.Persistance.Dtos.Security;
using Profex.Service.Common.Security;
using Profex.Service.Helpers;
using Profex.Service.Interfaces.MasterAuth;
using Profex.Service.Interfaces.Notifactions;

namespace Profex.Service.Services.MasterAuth
{
    public class MasterAuthService : IAuthMasterService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMasterRepository _masterRepository;
        
        private readonly ISmsSender _smsSender;
        private readonly ITokenMasterService _tokenMasterService;
        private const int CACHED_MINUTES_FOR_REGISTER = 60;
        private const int CACHED_MINUTES_FOR_VERIFICATION = 50;
        private const string REGISTER_CACHE_KEY = "register_";
        private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
        private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
        public MasterAuthService(IMemoryCache memoryCache,
            IMasterRepository masterRepository,
            ISmsSender smsSender,
            ITokenMasterService tokenMasterService)
        {
            this._memoryCache = memoryCache;
            this._masterRepository = masterRepository;
            this._smsSender = smsSender;
            this._tokenMasterService = tokenMasterService;
        }
        public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
        {
            var master = await _masterRepository.GetByPhoneAsync(dto.PhoneNumber);
            if (master is not null) throw new UserAlreadyExistException(dto.PhoneNumber);
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
            //throw new NotImplementedException();
            if(_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
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

                var smsResult = await _smsSender.SendAsync(smsMessage);
                if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
                else return (Result: false, CachedVerificationMinutes: 0);                
            }
            else throw new UserCacheDataExpiredException();
        }

        public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
        {
            throw new NotImplementedException();
            //if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))

            //{
            //    if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            //    {
            //        if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
            //            throw new VerificationTooManyRequestsException();
            //        else if (verificationDto.Code == code)
            //        {
            //            var dbResult = await RegisterToDatabaseAsync(registerDto);
            //            if (dbResult is true)
            //            {
            //                var master = await _masterRepository.GetByPhoneAsync(phone);
            //                string token = _tokenMasterService.GenerateToken(master);
            //                return (Result: true, Token: token);
            //            }
            //            else return (Result: false, Token: "");
            //        }
            //        else
            //        {
            //            _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
            //            verificationDto.Attempt++;
            //            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
            //                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
            //            return (Result: false, Token: "");
            //        }
            //    }
            //    else throw new VerificationCodeExpiredException();
            //}
            //else throw new UserCacheDataExpiredException();
        }
    }
    //private async Task<bool> RegisterToDatabaseAsync(RegisterDto registerDto)
    //{
    //    var master = new Master();
    //    master.FirstName = registerDto.FirstName;
    //    master.LastName = registerDto.LastName;
    //    master.PhoneNumber = registerDto.PhoneNumber;
    //    master.PhoneNumberConfirmed = true;
    //    master.ImagePath = master.ImagePath;
    //    var hasherResult = PasswordHasher.Hash(registerDto.Password);
    //    master.PasswordHash = hasherResult.Hash;
    //    master.Salt = hasherResult.Salt;
    //    master.IsFree = true;
    //    master.CreatedAt = master.UpdatedAt = TimeHelper.GetDateTime();
    //}
}