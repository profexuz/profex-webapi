namespace Profex.Domain.Entities.postRequests;

public class Request : Auditable
{
    public long MasterId { get; set; }
    public long PostId { get; set; }
    public long UserId { get; set; }
    public bool IsAccepted { get; set; } = false;
}
