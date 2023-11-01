using System.Security.Claims;
using FluentValidation;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
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
        IHttpContextAccessor httpCtxAccessor
    ) : UserService(
        repo,
        config,
        userStore,
        userManager,
        loginModelValidator,
        registrationModelValidator
    ), IUserService
{
    public override async Task<Option<UserDto>> GetCurrentUser(CancellationToken token)
    {
        if (httpCtxAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            is not string userIdStr
        )
        {
            return Option<UserDto>.None;
        }

        var userId = Guid.Parse(userIdStr);
        var user = await Repo.GetById(userId, token);
        return user.ToDto();
    }
}