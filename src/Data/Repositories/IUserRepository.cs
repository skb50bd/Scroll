using LanguageExt;
using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public interface IUserRepository : IRepository<Guid, User>
{
    Task<Option<User>> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Option<User>> GetByUserName(string userName, CancellationToken cancellationToken);
}