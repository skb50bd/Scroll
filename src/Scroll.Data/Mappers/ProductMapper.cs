using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Mappers;

public static class ProductMapper
{
    public static ModelBuilder MapProduct(this ModelBuilder builder)
    {
        builder
            .Entity<Product>()
            .HasKey(x => x.Id);

        builder
            .Entity<Product>()
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Entity<Product>()
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder
            .Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 6)
            .HasColumnType("decimal(18, 6)")
            .IsRequired();

        builder
            .Entity<Product>()
            .Property(p => p.AddedOn)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder
            .Entity<Product>()
            .Property(p => p.Link)
            .HasMaxLength(200)
            .IsRequired(false);

        builder
            .Entity<Product>()
            .Property(p => p.ImageName)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Entity<Product>()
            .Property(p => p.ClickCount)
            .IsRequired();

        builder
            .Entity<Product>()
            .Property(p => p.FavoriteCount)
            .IsRequired();

        builder
            .Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products);

        builder
            .Entity<Product>()
            .HasMany(p => p.Favorites)
            .WithOne(c => c.Product);

        builder
            .Entity<Product>()
            .HasIndex(p => p.Title)
            .IsUnique()
            .HasDatabaseName("IX_Product_Title");

        builder
            .Entity<Product>()
            .HasIndex(p => p.Price)
            .HasDatabaseName("IX_Product_Price");

        builder
            .Entity<Product>()
            .HasIndex(p => new { p.FavoriteCount, p.ClickCount })
            .HasDatabaseName("IX_Product_Engagement");

        return builder;
    }
}