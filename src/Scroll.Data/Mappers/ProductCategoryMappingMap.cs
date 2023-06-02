using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Library.FakeData;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Mappers;

public class ProductCategoryMappingMap : IEntityTypeConfiguration<ProductCategoryMapping>
{
    public void Configure(EntityTypeBuilder<ProductCategoryMapping> builder)
    {
        builder
            .HasKey(pcm =>
                new
                {
                    pcm.ProductId,
                    pcm.CategoryId
                });

        builder
            .HasOne(pcm => pcm.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(c => c.CategoryId);

        builder
            .HasOne(pcm => pcm.Product)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(p => p.ProductId);

        builder
            .HasData(FakeData.ProductCategories);
    }
}