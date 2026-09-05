namespace greenslip_backend.DTOs;

public class InvoiceIngestDto
{
    public int StoreId {get; set;}
    public string InvoiceNo {get; set;} = string.Empty;
    public string IdempotencyKey {get; set;} = string.Empty;
    public string? BillTo {get; set;} 
    public string? CustomerPhone {get; set;}
    public string PaymentMode {get; set;} = "Cash";
    public List<InvoiceItemIngestDto> Items {get; set;} = new List<InvoiceItemIngestDto>();
    public InvoiceSummaryIngestDto Summary {get; set;} = new InvoiceSummaryIngestDto();

}