using Microsoft.AspNetCore.Mvc;
using greenslip_backend.Data;
using greenslip_backend.DTOs;
using greenslip_backend.Models;
using Microsoft.EntityFrameworkCore;

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


    // Invoice Post Req

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
          ShopName = dto.ShopName,
          ShopAddress = dto.ShopAddress,
          ShopPhone = dto.ShopPhone,
          CashierName = dto.CashierName,
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


    // Invoice Get Request

    [HttpGet("{hash}")]
    public IActionResult GetReceipt(string hash)
    {
        var invoice = _context.Invoices
                      .Include(i => i.Items)
                      .FirstOrDefault(i => i.ReceiptHash == hash);
        
        if(invoice == null)
        {
            return NotFound(new {success = false , error = "Receipt Not Found"});
        }

        return Ok( new {success = true , invoice = invoice });
    }


    // Feedback Req
    [HttpPost("{hash}/feedback")]
    public IActionResult SubmitFeedback(string hash, [FromBody] FeedBackDto dto)
    {
        var invoice = _context.Invoices.FirstOrDefault(i => i.ReceiptHash == hash);

        if (invoice == null)
        {
            return NotFound(new { success = false , error = "Invoice Not Found"});
        }

        bool isValidRating = Enum.TryParse<Rating>(dto.Rating , true , out Rating parsedRating);

        if (!isValidRating)
        {
            return BadRequest(new { success = false , error = "Rating must be one of : worst , not_good , fine , good , best"});
        }


        var existingFeedback = _context.Feedbacks.FirstOrDefault(f => f.InvoiceId == invoice.Id);

        if(existingFeedback != null)
        {
            return Conflict(new { success = false , error = "Feedback Already Submitted for this receipt"});
        }

        var feedback = new Feedback
        {
            InvoiceId = invoice.Id,
            InvoiceNo = invoice.InvoiceNo,
            ShopName  = invoice.ShopName,
            Rating    = parsedRating,
            Comment   = dto.Comment
        };

        _context.Feedbacks.Add(feedback);
        _context.SaveChanges();

        return Ok(new {success = true , feedback = new { rating = feedback.Rating.ToString() , comment = feedback.Comment, submittedAt = feedback.SubmittedAt }});

    }

}