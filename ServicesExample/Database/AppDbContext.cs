using Microsoft.EntityFrameworkCore;
using ServicesExample.Entities;

namespace ServicesExample.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Author> Authors { get; set; }
    //public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().UseTpcMappingStrategy();
        
        modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
        
        modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .HasMaxLength(50)
            .HasMinLength(3);
        
        base.OnModelCreating(modelBuilder);
    }
}