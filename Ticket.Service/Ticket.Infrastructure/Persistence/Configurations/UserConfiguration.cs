using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entities;

namespace Ticket.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        // Add default value
        builder.HasIndex(u => u.Username).HasDatabaseName("IX_User_Username").IsUnique();
        builder.Property(u => u.Username).IsRequired().HasMaxLength(255);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(255);
        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
    }
}
