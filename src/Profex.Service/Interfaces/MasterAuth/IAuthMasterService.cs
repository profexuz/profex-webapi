using Profex.Persistance.Dtos.MasterAuth;

namespace Profex.Service.Interfaces.MasterAuth
{
    public interface IAuthMasterService
    {
        public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto);
        public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone);
        public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code);
        public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
    }
}
