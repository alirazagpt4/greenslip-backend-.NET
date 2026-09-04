namespace greenslip_backend.Models;

public class InvoiceItem
{
    public int Id {get; set;}
    public int InvoiceId {get; set;}
    public Invoice? Invoice {get; set;}
    public string ProductName {get; set;} = string.Empty;
    public string? Color {get; set;}
    public string? Size {get; set;}
    public string ItemName { get; set; } = string.Empty;
    public int Quantity {get; set;}
    public decimal UnitPrice {get; set;}
    public decimal GstPercent {get; set;}
    public decimal TotalPrice {get; set;}

}