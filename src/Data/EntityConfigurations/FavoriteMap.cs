using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Domain;
using Scroll.Domain.Entities;

namespace Scroll.Data.EntityConfigurations;

public class FavoriteMap: IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder
            .HasKey(x => new { x.UserId, x.ProductId });

        builder.Property(x => x.ProductId)
            .HasConversion(
                v => v.Value,
                v => new ProductId(v)
            );

        builder
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);

        builder
            .HasOne(f => f.Product)
            .WithMany(p => p.Favorites)
            .HasForeignKey(f => f.ProductId);
    }
}