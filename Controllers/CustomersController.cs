using Microsoft.AspNetCore.Mvc;
using greenslip_backend.Data;
using greenslip_backend.DTOs;
using System.Text.Json;

namespace greenslip_backend.Controllers;

[ApiController]
[Route("api/v1/customers")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;


    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("customers-list")]
    public IActionResult GetCustomersList()
    {
        var segments = _context.CustomerSegments.ToList();

        var allCustomers = new List<CustomerListEntryDto>();

        foreach (var segment in segments)
        {
            var customers = JsonSerializer.Deserialize<List<CustomerListEntryDto>>(segment.CustomerList);

            if(customers != null)
            {
                allCustomers.AddRange(customers);
            }

        }

        var uniqueCustomers = allCustomers
        .GroupBy(c => c.CustomerPhone)
        .Select(g => g.First())
        .ToList();

        return Ok(new {success=true , totalCount = uniqueCustomers.Count , data = uniqueCustomers});
    }
}