using System.Security.Claims;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Scroll.Core.Extensions;
using Scroll.Core.ObjectMapping;
using Scroll.Core.Services;
using Scroll.Data.Repositories;
using Scroll.Domain.DTOs;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;

namespace Scroll.Api;

public class HttpUserService(
        IUserRepository repo,
        IConfiguration config,
        UserManager<User> userManager,
        IUserStore<User> userStore,
        IValidator<LoginModel> loginModelValidator,
        IValidator<UserRegistrationModel> registrationModelValidator,
        IHttpContextAccessor httpCtxAccessor,
        SignInManager<User> signInManager
    ) : UserService(
        repo,
        config,
        userStore,
        userManager,
        loginModelValidator,
        registrationModelValidator
    ), IUserService
{
    private readonly IHttpContextAccessor _httpCtxAccessor = httpCtxAccessor;
    private readonly SignInManager<User> _signInManager = signInManager;

    public override async Task<UserDto?> GetCurrentUser(CancellationToken token)
    {
        if (_httpCtxAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            is not string userIdStr
        )
        {
            return null;
        }

        var userId = Guid.Parse(userIdStr);
        var user = await Repo.GetById(userId, token);
        return user.ToDto();
    }

    public override Task<Result<UserDto>> Login(LoginModel model, CancellationToken token) =>
        AuthenticateAndGetUser(model, token).MapAsync(async user =>
        {
            await _signInManager
                .SignInAsync(
                    user,
                    false,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

            return user.ToDto();
        });
}