using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Domain.Entities;

namespace Scroll.Data.EntityConfigurations;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.FullName)
            .HasMaxLength(100);

        builder
            .Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.NormalizedUserName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(x => x.NormalizedEmail)
            .IsRequired()
            .HasMaxLength(150);

        builder
            .Property(x => x.ConcurrencyStamp)
            .HasMaxLength(128)
            .IsRowVersion();

        builder
            .Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(x => x.SecurityStamp)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(x => x.PhoneNumber)
            .HasMaxLength(50);

        builder
            .HasIndex(u => u.Email)
            .HasDatabaseName("IX_Users_Email");

        builder
            .HasIndex(u => u.UserName)
            .HasDatabaseName("IX_Users_UserName");

        builder
            .HasIndex(u => u.PhoneNumber)
            .HasDatabaseName("IX_Users_Phone");
    }
}
