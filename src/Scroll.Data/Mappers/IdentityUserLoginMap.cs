using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scroll.Data.Mappers;

public class IdentityUserLoginMap : IEntityTypeConfiguration<IdentityUserLogin<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
    {
        builder
            .HasKey(x => new { x.ProviderKey, x.LoginProvider });

        builder
            .Property(x => x.ProviderKey)
            .HasMaxLength(128);

        builder
            .Property(x => x.LoginProvider)
            .HasMaxLength(128);

        builder
            .Property(x => x.ProviderDisplayName)
            .HasMaxLength(128);

        builder
            .HasIndex(x => x.UserId)
            .HasDatabaseName("IX_UserLogins_User");
    }
}
