using Microsoft.EntityFrameworkCore;
using greenslip_backend.Models;

namespace greenslip_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Invoice> Invoices {get; set;}
    public DbSet<InvoiceItem> InvoiceItems {get; set;}
    public DbSet<Feedback> Feedbacks {get; set;}
    public DbSet<CustomerSegment> CustomerSegments {get; set;}

    
}