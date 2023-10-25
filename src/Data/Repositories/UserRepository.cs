using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public class UserRepository(
        AppDbContext dbCtx,
        ILogger<UserRepository> logger
    ) : Repository<User>(dbCtx, logger), IUserRepository
{
    public Task<User?> GetByEmail(string email) =>
        Table.FirstOrDefaultAsync(u => u.Email == email);

    public Task<User?> GetByUserName(string userName) =>
        Table.FirstOrDefaultAsync(u => u.UserName == userName);
}