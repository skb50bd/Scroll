using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Library.FakeData;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Mappers;

public class ProductMap: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder
            .Property(p => p.Price)
            .HasPrecision(18, 6)
            .HasColumnType("decimal(18, 6)")
            .IsRequired();

        builder
            .Property(p => p.AddedOn)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder
            .Property(p => p.Link)
            .HasMaxLength(200)
            .IsRequired(false);

        builder
            .Property(p => p.ImageName)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(p => p.ClickCount)
            .IsRequired();

        builder
            .Property(p => p.FavoriteCount)
            .IsRequired();

        builder
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products);

        builder
            .HasMany(p => p.Favorites)
            .WithOne(c => c.Product);

        builder
            .HasMany(p => p.ProductCategories)
            .WithOne(pcm => pcm.Product);

        builder
            .HasIndex(p => p.Title)
            .IsUnique()
            .HasDatabaseName("IX_Product_Title");

        builder
            .HasIndex(p => p.Price)
            .HasDatabaseName("IX_Product_Price");

        builder
            .HasIndex(p => new { p.FavoriteCount, p.ClickCount })
            .HasDatabaseName("IX_Product_Engagement");

        builder
            .HasData(FakeData.Products);
    }
}