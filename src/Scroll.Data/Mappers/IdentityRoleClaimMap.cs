using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scroll.Data.Mappers;

public class IdentityRoleClaimMap : IEntityTypeConfiguration<IdentityRoleClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.ClaimType)
            .HasMaxLength(128);

        builder
            .Property(x => x.ClaimValue)
            .HasMaxLength(128);
    }
}
