namespace Profex.Persistance.Validations.Dtos.Notifications;

public class SmsMessage
{
    public string Recipent { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
