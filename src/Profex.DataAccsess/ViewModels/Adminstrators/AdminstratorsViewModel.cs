using Profex.Domain.Entities;

namespace Profex.DataAccsess.ViewModels.Adminstrators;

public class AdminstratorsViewModel : Auditable
{
    public string First_name { get; set; } = string.Empty;
    public string Last_name { get; set; } = string.Empty;
    public string Phone_number { get; set; } = string.Empty;
    public bool Phone_number_confirmed { get; set; }
    public string Image_path { get; set; } = string.Empty;
}
