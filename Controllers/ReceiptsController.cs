using Microsoft.AspNetCore.Mvc;
using greenslip_backend.Data;
using greenslip_backend.DTOs;
using greenslip_backend.Models;

namespace greenslip_backend.Controllers;


[ApiController]
[Route("api/v1/receipts")]
public class ReceiptsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReceiptsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("ingest")]
    public IActionResult Ingest([FromBody] InvoiceIngestDto dto)
    {
        
        var existing = _context.Invoices.FirstOrDefault(i => i.IdempotencyKey == dto.IdempotencyKey);

        if(existing != null)
        {
            return Ok(new { success=true , duplicate=true , receiptHash = existing.ReceiptHash});
        }

        string receiptHash = Guid.NewGuid().ToString("N");

        var invoice = new Invoice
        {
          ReceiptHash = receiptHash,
          InvoiceNo = dto.InvoiceNo,
          IdempotencyKey = dto.IdempotencyKey,
          StoreId = dto.StoreId,
          CustomerName = dto.BillTo,
          CustomerPhone = dto.CustomerPhone,
          PaymentMode = dto.PaymentMode,
          TotalAmount = dto.Summary.Total,
          Discount = dto.Summary.Discount,
          GstAmount = dto.Summary.Gst,
          PosFee = dto.Summary.PosFee,
          PayableAmount = dto.Summary.Payable
        };

        foreach(var item in dto.Items)
        {
            var invoiceItem = new InvoiceItem
            {
                ProductName = item.Name,
                ItemName = item.Name,
                Quantity = item.Qty,
                UnitPrice = item.Price,
                GstPercent = item.GstPercent,
                TotalPrice = item.Price * item.Qty
            };

            invoice.Items.Add(invoiceItem);
        }

        _context.Invoices.Add(invoice);
        _context.SaveChanges();

        return Ok( new { success = true , duplicate = false , receiptHash = receiptHash , invoiceNo = invoice.InvoiceNo , payableAmount = invoice.PayableAmount});
    }
}