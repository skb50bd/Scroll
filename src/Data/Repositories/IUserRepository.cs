using Scroll.Domain.Entities;

namespace Scroll.Data.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmail(string email);
    Task<User?> GetByUserName(string userName);
}