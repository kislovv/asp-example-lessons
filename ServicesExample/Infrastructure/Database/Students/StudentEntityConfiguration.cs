using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesExample.Domain.Entities;

namespace ServicesExample.Infrastructure.Database.Students;

public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.Name)
            .HasMinLength(3)
            .HasMaxLength(50);
    }
}