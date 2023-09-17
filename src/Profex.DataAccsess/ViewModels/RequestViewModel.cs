namespace Profex.DataAccsess.ViewModels;

public class RequestViewModel
{
    public long Id { get; set; }
    public long postId { get; set; }
    public long userId { get; set; }
    public bool isAccepted { get; set; }
    public long[]? mastersId { get; set; }
}
