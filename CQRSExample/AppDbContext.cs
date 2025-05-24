using Microsoft.EntityFrameworkCore;

namespace CQRSExample;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
{
    public DbSet<Product> Products { get; set; }
}