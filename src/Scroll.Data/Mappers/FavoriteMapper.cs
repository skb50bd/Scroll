using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Mappers;

public static class FavoriteMapper
{
    public static ModelBuilder MapFavorite(this ModelBuilder builder)
    {
        builder
            .Entity<Favorite>()
            .HasKey(x => new { x.UserId, x.ProductId });

        builder
            .Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);

        builder
            .Entity<Favorite>()
            .HasOne(f => f.Product)
            .WithMany(p => p.Favorites)
            .HasForeignKey(f => f.ProductId);

        return builder;
    }
}