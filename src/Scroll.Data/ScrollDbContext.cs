using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scroll.Data.Mappers;
using Scroll.Library.Models.Entities;

namespace Scroll.Data;

public class ScrollDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
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
    }
}