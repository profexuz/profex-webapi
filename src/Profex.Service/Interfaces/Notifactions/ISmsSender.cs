using Profex.Persistance.Dtos.Notifications;

namespace Profex.Service.Interfaces.Notifactions
{
    public interface ISmsSender
    {
        public Task<bool> SendAsync(SmsMessage smsMessage);
    }
}