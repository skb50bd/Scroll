using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scroll.Data.Mappers;
using Scroll.Library.Models.Entities;

namespace Scroll.Data;

public class ScrollDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ScrollDbContext(
        DbContextOptions<ScrollDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfigurationsFromAssembly(
                typeof(ScrollDbContext).Assembly);

        builder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<ProductCategoryMapping>();

        builder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithMany(p => p.Categories)
            .UsingEntity<ProductCategoryMapping>();
    }
}