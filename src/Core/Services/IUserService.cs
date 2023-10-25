using System.IdentityModel.Tokens.Jwt;
using LanguageExt.Common;
using Scroll.Domain.DTOs;
using Scroll.Domain.InputModels;

namespace Scroll.Core.Services;

public interface IUserService
{
    Task<UserDto?> GetByEmail(string email);
    Task<UserDto?> GetByUserName(string userName);
    Task<Result<UserDto>> Register(UserRegistrationModel model);
    Task<UserDto?> GetCurrentUser();
    Task<Result<UserDto>> Login(LoginModel login);
    Task<Result<JwtSecurityToken>> CreateAccessToken(LoginModel login);
}