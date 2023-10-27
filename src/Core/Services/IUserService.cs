using System.IdentityModel.Tokens.Jwt;
using LanguageExt.Common;
using Scroll.Domain.DTOs;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Services;

public interface IUserService
{
    Task<UserDto?> GetByEmail(string email, CancellationToken token);
    Task<UserDto?> GetByUserName(string userName, CancellationToken token);
    Task<Result<UserDto>> Register(UserRegistrationModel model, CancellationToken token);
    Task<UserDto?> GetCurrentUser(CancellationToken token);
    Task<Result<UserDto>> Login(LoginModel login, CancellationToken token);
    Task<Result<JwtSecurityToken>> CreateAccessToken(LoginModel login, CancellationToken token);
}