using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Scroll.Core.ObjectMapping;
using Scroll.Data.Repositories;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;
using Scroll.Domain.Exceptions;
using Scroll.Domain.InputModels;

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

    public abstract Task<Option<UserDto>> GetCurrentUser(CancellationToken token);

    public Task<Option<UserDto>> GetByEmail(string email, CancellationToken token) =>
        Repo.GetByEmail(email, token).ToDtoAsync();

    public async Task<Option<UserDto>> GetByUserName(string userName, CancellationToken token) =>
        (await Repo.GetByUserName(userName, token)).ToDto();

    public async Task<Result<UserDto>> Register(UserRegistrationModel model, CancellationToken token)
    {
        var validationResult = await _registrationModelValidator.ValidateAsync(model, token);

        if (validationResult.IsValid is false)
        {
            return new(new ValidationException(validationResult.Errors));
        }

        var user = new User
        {
            FullName = model.FullName
        };

        await _userStore.SetUserNameAsync(user, model.UserName, token);
        await _userStore.SetNormalizedUserNameAsync(user, model.UserName, token);
        await ((IUserEmailStore<User>)_userStore).SetEmailAsync(user, model.Email, token);

        var identityResult =
            await _userManager.CreateAsync(user, model.Password);

        if (identityResult.Succeeded is false)
        {
            return new(new IdentityException(identityResult.Errors));
        }

        return Option<User>.Some(user).ToDto().ValueUnsafe();
    }
}