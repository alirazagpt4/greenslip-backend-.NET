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
        return Ok( new { success = true , message = "End Point Hits"});
    }
}