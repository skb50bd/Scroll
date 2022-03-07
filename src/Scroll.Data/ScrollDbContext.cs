using Microsoft.EntityFrameworkCore;
using Scroll.Data.Mappers;

namespace Scroll.Data;

public class ScrollDbContext : DbContext
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