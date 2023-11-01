using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scroll.Domain.Entities;

namespace Scroll.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options), IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

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

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken
    )
    {
        ConvertDateTimeOffsetToUtc();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void ConvertDateTimeOffsetToUtc()
    {
        var changedEntries =
            ChangeTracker
                .Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in changedEntries)
        {
            foreach (var property in entry.Properties)
            {
                if (property.CurrentValue is DateTimeOffset dateTimeOffset)
                {
                    property.CurrentValue = dateTimeOffset.ToUniversalTime();
                }
            }
        }
    }
}
