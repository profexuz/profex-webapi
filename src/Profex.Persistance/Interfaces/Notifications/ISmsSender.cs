using Profex.Persistance.Dtos.Notifications;

namespace Profex.Persistance.Interfaces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}
