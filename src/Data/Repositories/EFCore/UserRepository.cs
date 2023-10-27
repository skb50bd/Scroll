using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories.EFCore;

public class UserRepository(
        AppDbContext dbCtx,
        ILogger<UserRepository> logger
    ) : Repository<User>(dbCtx, logger), IUserRepository
{
    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken) =>
        Table.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public Task<User?> GetByUserName(string userName, CancellationToken cancellationToken) =>
        Table.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
}