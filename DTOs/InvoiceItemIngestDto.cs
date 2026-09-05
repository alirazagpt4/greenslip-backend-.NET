namespace greenslip_backend.DTOs;

public class InvoiceItemIngestDto
{
    public string Name {get; set;} = string.Empty;
    public int Qty {get; set;}
    public decimal Price {get; set;}
    public decimal GstPercent {get; set;}

}

