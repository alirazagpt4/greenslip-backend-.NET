using Microsoft.AspNetCore.Mvc;
using greenslip_backend.Data;
using greenslip_backend.DTOs;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace greenslip_backend.Controllers;

[ApiController]
[Route("api/v1/admin")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AdminController(AppDbContext context , IConfiguration config)
    {
           _context = context;
           _config = config;
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

        // check for password that comes from req body
         bool IsPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password , admin.PasswordHash);

         if(!IsPasswordValid){
            return Unauthorized(new {success = false , error = "Invalid Password."});
         }


        // add claims
        var claims = new List<Claim>
        {
            new Claim("AdminId" , admin.Id.ToString()),
            new Claim("IsSuperAdmin" , admin.IsSuperAdmin.ToString()),
            new Claim("CompanyId" , admin.CompanyId?.ToString() ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // token creation
        var token = new JwtSecurityToken(
            issuer : _config["Jwt:Issuer"],
            claims : claims,
            expires : DateTime.Now.AddHours(24),
            signingCredentials : creds
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        
        return Ok(new {success = true , token = tokenString});
    }
}