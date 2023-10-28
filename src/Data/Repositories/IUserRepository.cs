using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public interface IUserRepository : IRepository<Guid, User>
{
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<User?> GetByUserName(string userName, CancellationToken cancellationToken);
}