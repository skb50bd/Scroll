using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Mappers;

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
            .HasMany(c => c.Products)
            .WithMany(p => p.Categories);

        builder
            .HasIndex(c => c.Name)
            .IsUnique()
            .HasDatabaseName("IX_Category_Name");
    }
}