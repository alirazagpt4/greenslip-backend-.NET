using Microsoft.AspNetCore.Mvc;
using greenslip_backend.Data;
using greenslip_backend.DTOs;


namespace greenslip_backend.Controllers;

[ApiController]
[Route("api/v1/admin")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
           _context = context;
    }

    // login admin request
    [HttpPost("login")]
    public IActionResult Login([FromBody] AdminLoginDto dto)
    {
        var admin = _context.Admins.FirstOrDefault(a => a.Username == dto.Username);

        if(admin == null)
        {
            return Unauthorized(new {success = false , error = "Invalid Credentials."});
        }

        

        return Ok(new {success = true , message = "Admin mil gaya laikin abhi password check nahi hua "});
    }
}