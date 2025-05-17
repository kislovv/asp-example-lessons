using Microsoft.EntityFrameworkCore;
using ServicesExample.Domain.Entities;
using ServicesExample.Infrastructure.Database.Events;
using ServicesExample.Infrastructure.Database.Students;
using ServicesExample.Infrastructure.Database.Users;

namespace ServicesExample.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}