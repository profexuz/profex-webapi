namespace Profex.Service.Interfaces.Identity;

public interface IIdentityService
{
    public long UserId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string IdentityRole { get; }
}
