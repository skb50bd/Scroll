using Microsoft.EntityFrameworkCore;
using Scroll.Library.Models.Entities;

namespace Scroll.Data.Mappers;

public static class CategoryMapper
{
    public static ModelBuilder MapCategory(this ModelBuilder builder)
    {
        builder
            .Entity<Category>()
            .HasKey(x => x.Id);

        builder
            .Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Entity<Category>()
            .HasMany(c => c.Products)
            .WithMany(p => p.Categories);

        builder
            .Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique()
            .HasDatabaseName("IX_Category_Name");

        return builder;
    }
}