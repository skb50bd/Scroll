using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scroll.Domain.Entities;

namespace Scroll.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<ProductCategoryMapping>();

        builder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithMany(p => p.Categories)
            .UsingEntity<ProductCategoryMapping>();

        base.OnModelCreating(builder);
    }
}
