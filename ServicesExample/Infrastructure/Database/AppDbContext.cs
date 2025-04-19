using Microsoft.EntityFrameworkCore;
using ServicesExample.Domain.Entities;

namespace ServicesExample.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .UseTpcMappingStrategy()
            .HasIndex(user => user.Login);
        
        modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
        
        modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .HasMinLength(3)
            .HasMaxLength(50);
        
        base.OnModelCreating(modelBuilder);
    }
}