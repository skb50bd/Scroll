using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scroll.Data.Mappers;

namespace Scroll.Data;

public class ScrollDbContext : IdentityDbContext
{
    public ScrollDbContext(
        DbContextOptions<ScrollDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .MapProduct()
            .MapCategory()
            .MapFavorite();
    }
}