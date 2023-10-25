using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Domain.Entities;
using Scroll.Domain.FakeData;

namespace Scroll.Data.EntityConfigurations;

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