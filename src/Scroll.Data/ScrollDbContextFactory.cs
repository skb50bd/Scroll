using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Scroll.Data;

public class ScrollDbContextFactory : IDesignTimeDbContextFactory<ScrollDbContext>
{
    public ScrollDbContext CreateDbContext(string[] args)
    {
        const string connectionString = 
            "Host=localhost;" +
            "Port=5432;" +
            "Database=scroll1;" +
            "Username=scrolladmin;" +
            "Password=scrolladmin";

        var builder = new DbContextOptionsBuilder<ScrollDbContext>();
        builder.UseNpgsql(connectionString);
        return new ScrollDbContext(builder.Options);
    }
}