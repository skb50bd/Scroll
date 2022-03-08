using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Scroll.Data.Mappers;

public class IdentityUserTokenMap : IEntityTypeConfiguration<IdentityUserToken<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
    {
        builder
            .HasKey(t =>
                new
                {
                    t.UserId,
                    t.LoginProvider,
                    t.Name
                });

        builder
            .Property(t => t.LoginProvider)
            .HasMaxLength(128);

        builder
            .Property(t => t.Name)
            .HasMaxLength(128);

        builder
            .Property(t => t.Value)
            .HasMaxLength(1024);
    }
}