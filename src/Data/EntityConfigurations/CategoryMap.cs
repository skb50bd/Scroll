using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Domain.Entities;

namespace Scroll.Data.EntityConfigurations;

public class CategoryMap: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .HasMany(c => c.Products)
            .WithMany(p => p.Categories);

        builder
            .HasMany(c => c.ProductCategories)
            .WithOne(pcm => pcm.Category);

        builder
            .HasIndex(c => c.Name)
            .IsUnique()
            .HasDatabaseName("IX_Category_Name");
    }
}