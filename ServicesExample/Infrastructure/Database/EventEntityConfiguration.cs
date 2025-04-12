using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesExample.Domain.Entities;

namespace ServicesExample.Infrastructure.Database;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(150);
        
        builder.Property(e => e.Quota)
            .HasDefaultValue(1);
    }
}