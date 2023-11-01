using LanguageExt;
using LanguageExt.Common;
using Scroll.Domain.DTOs;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Services;

public interface IUserService
{
    Task<Option<UserDto>> GetByEmail(string email, CancellationToken token);
    Task<Option<UserDto>> GetByUserName(string userName, CancellationToken token);
    Task<Result<UserDto>> Register(UserRegistrationModel model, CancellationToken token);
    Task<Option<UserDto>> GetCurrentUser(CancellationToken token);
}