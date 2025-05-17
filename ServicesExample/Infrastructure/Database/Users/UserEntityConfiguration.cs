using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicesExample.Domain.Entities;

namespace ServicesExample.Infrastructure.Database.Users;

public class UserEntityConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.UseTpcMappingStrategy()
            .HasIndex(user => user.Login);

        builder.Property(u => u.Role)
            .HasConversion(
                v => v.ToString(),
                v => (UserRole)Enum.Parse(typeof(UserRole), v));
    }
}