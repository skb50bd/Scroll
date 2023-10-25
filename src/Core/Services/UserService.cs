using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Scroll.Core.Extensions;
using Scroll.Core.ObjectMapping;
using Scroll.Data.Repositories;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;
using Scroll.Domain.Exceptions;
using Scroll.Domain.InputModels;
using Throw;

namespace Scroll.Core.Services;

public abstract class UserService : IUserService
{
    protected readonly IUserRepository Repo; 
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;
    private readonly IUserStore<User> _userStore;
    private readonly IValidator<UserRegistrationModel> _registrationModelValidator;
    private readonly IValidator<LoginModel> _loginModelValidator;
    
    protected UserService(
        IUserRepository repo,
        IConfiguration config, 
        IUserStore<User> userStore,
        UserManager<User> userManager, 
        IValidator<LoginModel> loginModelValidator, 
        IValidator<UserRegistrationModel> registrationModelValidator
    )
    {
        Repo                        = repo;
        _config                     = config;
        _userStore                  = userStore;
        _userManager                = userManager;
        _loginModelValidator        = loginModelValidator;
        _registrationModelValidator = registrationModelValidator;
    }
    
    public abstract Task<UserDto?> GetCurrentUser();

    public Task<UserDto?> GetByEmail(string email) =>
        Repo.GetByEmail(email).ToDtoAsync();

    public Task<UserDto?> GetByUserName(string userName) =>
        Repo.GetByUserName(userName).ToDtoAsync();

    public async Task<Result<UserDto>> Register(UserRegistrationModel model)
    {
        var validationResult = await _registrationModelValidator.ValidateAsync(model);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var user = new User
        {
            FullName = model.FullName
        };

        await _userStore.SetUserNameAsync(user, model.UserName, CancellationToken.None);
        await _userStore.SetNormalizedUserNameAsync(user, model.UserName, CancellationToken.None);
        await ((IUserEmailStore<User>)_userStore).SetEmailAsync(user, model.Email, CancellationToken.None);
        
        var identityResult = 
            await _userManager.CreateAsync(user, model.Password);

        if (identityResult.Succeeded is false)
        {
            return new(new IdentityException(identityResult.Errors));
        }

        return user.ToDto();
    }

    public virtual Task<Result<UserDto>> Login(LoginModel _) => throw new NotImplementedException();

    protected async Task<Result<User>> AuthenticateAndGetUser(LoginModel login)
    {
        var validationResult =
            await _loginModelValidator.ValidateAsync(login);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }
        
        var user = 
            await Repo.GetByUserName(login.UserName);

        if (user is null)
        {
            return new(StandardErrors.InvalidCredentials);
        }
        
        var passwordMatches = 
            await _userManager.CheckPasswordAsync(user, login.Password);

        if (passwordMatches is false)
        {
            return new(StandardErrors.InvalidCredentials);
        }

        return user;
    }

    public Task<Result<JwtSecurityToken>> CreateAccessToken(LoginModel login) =>
        AuthenticateAndGetUser(login).MatchAsync(
            user =>
            {
                var jwtIssuer = _config["Jwt:Issuer"];
                jwtIssuer.ThrowIfNull();

                var jwtAudience = _config["Jwt:Audience"];
                jwtAudience.ThrowIfNull();

                var jwtKey = _config["Jwt:Key"];
                jwtKey.ThrowIfNull();

                var securityKey = jwtKey.ToUtf8SymmetricSecurityKey();
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var names = user.FullName?.Split();
                var claims = new Claim[]
                {
                    new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new(JwtRegisteredClaimNames.NameId, user.UserName!),
                    new(JwtRegisteredClaimNames.Email, user.Email!),
                    new(JwtRegisteredClaimNames.Name, user.FullName!),
                    new(JwtRegisteredClaimNames.GivenName, names?.First() ?? string.Empty),
                    new(JwtRegisteredClaimNames.FamilyName, names?.Last() ?? string.Empty),
                    new(JwtRegisteredClaimNames.AuthTime, DateTime.UtcNow.ToString("O")),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token =
                    new JwtSecurityToken(
                        issuer:             jwtIssuer,
                        audience:           jwtAudience,
                        signingCredentials: credentials,
                        claims:             claims
                    );

                return token.RequireNotNull();
            },
            exn => new Result<JwtSecurityToken>(exn)
        );
}