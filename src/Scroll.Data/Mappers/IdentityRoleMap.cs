using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scroll.Data.Mappers;

public class IdentityRoleMap : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(128);

        builder
            .Property(x => x.NormalizedName)
            .HasMaxLength(128);

        builder
            .Property(x => x.ConcurrencyStamp)
            .HasMaxLength(128);
    }
}
