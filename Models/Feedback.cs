namespace greenslip_backend.Models;

public enum Rating
{
    Worst,
    NotGood,
    Fine,
    Good,
    Best
}


public class Feedback
{
    public int Id {get; set;}
    public int InvoiceId {get; set;}
    public Invoice? Invoice {get; set;}
    public string? InvoiceNo {get; set;}
    public string? ShopName {get; set;}
    public Rating Rating {get; set;}
    public string? Comment {get; set;}
    public DateTime SubmittedAt {get; set;} = DateTime.Now;

}