namespace greenslip_backend.DTOs;

public class InvoiceSummaryIngestDto
{
    public decimal Total {get; set;}
    public decimal Discount {get; set;} = 0;
    public decimal Gst {get; set;}
    public decimal PosFee {get; set;} = 1.0m;
    public decimal Payable {get; set;}
}