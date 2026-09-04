using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace greenslip_backend.Pages;

public class IndexModel : PageModel
{
    public void OnGet()
    {
         Console.WriteLine("Welcome to Greenslip Backend");
    }
}
