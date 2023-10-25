using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Domain.Entities;

namespace Scroll.Data.EntityConfigurations;

public class FavoriteMap: IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder
            .HasKey(x => new { x.UserId, x.ProductId });

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