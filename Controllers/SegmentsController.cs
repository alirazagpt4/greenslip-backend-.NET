using Microsoft.AspNetCore.Mvc;
using greenslip_backend.Data;
using greenslip_backend.DTOs;
using greenslip_backend.Models;
using System.Text.Json;

namespace greenslip_backend.Controllers;

[ApiController]
[Route("api/v1/segments")]
public class SegmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SegmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateSegment([FromBody] SegmentDto dto)
    {
        if (string.IsNullOrEmpty(dto.SegmentName))
        {
            return BadRequest(new { success=false , message = "Segment name required."});
        }

        if(dto.CustomerList == null || dto.CustomerList.Count == 0)
        {
            return BadRequest(new { success=false , message = "Customer list cannot be empty."});
        }

        var segment = new CustomerSegment
        {
            SegmentName = dto.SegmentName,
            FilterCriteria = dto.FilterCriteria == null ? null : JsonSerializer.Serialize(dto.FilterCriteria),
            CustomerList = JsonSerializer.Serialize(dto.CustomerList)
        };

        _context.CustomerSegments.Add(segment);
        _context.SaveChanges();

        return StatusCode(201 , new { success = true , message = "Segment saved successfully." , data = segment});
    }

    [HttpGet]
    public IActionResult GetSegments()
    {
        var segments = _context.CustomerSegments
        .OrderByDescending(s => s.CreatedAt)
        .ToList();

        return Ok(new { success=true , count = segments.Count , segments = segments});
    }
}
