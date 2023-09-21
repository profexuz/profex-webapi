namespace Profex.Service.Interfaces.MasterAuth;

public interface IForgetPasswordMaster
{
    Task<bool> SendCodePhoneNumberAsync(string phoneNumber);
    Task<(bool Result, string Token)> ConfirmCodeAsync(int code);
}
