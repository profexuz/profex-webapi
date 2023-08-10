using Profex.Persistance.Dtos.Notifications;
using Profex.Persistance.Interfaces.Notifications;

namespace Profex.Service.Services.Notifications;

public class SmsSender : ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage)
    {
        throw new NotImplementedException();
    }
}
