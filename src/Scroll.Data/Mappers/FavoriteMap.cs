using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Mappers;

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