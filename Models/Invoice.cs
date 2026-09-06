namespace greenslip_backend.Models;

public class Invoice
{
    public int Id { get;  set;}
    public string ReceiptHash { get; set;} = string.Empty;
    public string InvoiceNo {get;  set;} = string.Empty;
    public string? FbrInvoiceNo {get; set;} = string.Empty;
    public string IdempotencyKey {get; set;} = string.Empty;
    public int StoreId {get;  set;}
    public string? ShopName {get;  set;}
    public string? ShopAddress {get;  set;}
    public string? ShopPhone {get;  set;}
    public string? CashierName {get;  set;}
    public string? CustomerName {get;  set;}
    public string? CustomerPhone {get;  set;}
    public decimal PriceExclTax {get;  set;} = 0.00m;
    public decimal TotalAmount {get;  set;}
    public decimal Discount {get;  set;} = 0.00m;
    public decimal GstAmount {get;  set;} 
    public decimal PosFee {get;  set;} = 1.00m;
    public decimal PayableAmount {get;  set;}
    public string  PaymentMode {get;  set;} = "Cash";
    public List<InvoiceItem> Items {get; set;} = new List<InvoiceItem>();
    public DateTime CreatedAt {get;  set;} = DateTime.Now;



}