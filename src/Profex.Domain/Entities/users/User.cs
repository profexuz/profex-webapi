namespace Profex.Domain.Entities.users;

public class User : Auditable
{
    public string First_name { get; set; } = string.Empty;

    public string Last_name { get; set; } = string.Empty;

    public string Phone_number { get; set; } = string.Empty;

    public bool Phone_number_confirmed { get; set; }

    public string Image_path { get; set; } = string.Empty;

    public string Password_hash { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;



}
