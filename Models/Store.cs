namespace greenslip_backend.Models;

public class Store
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public int CompanyId {get; set;}
    public Company? Company {get; set;}
}