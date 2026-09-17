namespace greenslip_backend.Models;

public class Admin
{
    public int Id {get; set;}
    public string Username {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
    public bool IsSuperAdmin {get; set;} = false;
    public int? CompanyId {get; set;}
    public Company? Company {get; set;}
}