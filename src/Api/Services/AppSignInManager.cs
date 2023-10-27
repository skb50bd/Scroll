using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Scroll.Domain.Entities;

namespace Scroll.Api.Services;

public class AppSignInManager(
    UserManager<User> userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<User> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<SignInManager<User>> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<User> confirmation
) : SignInManager<User>(
        userManager,
        contextAccessor,
        claimsFactory,
        optionsAccessor,
        logger,
        schemes,
        confirmation
    )
{
    public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user)
    {
        if (await base.CreateUserPrincipalAsync(user) is not ClaimsPrincipal principal)
        {
            throw new Exception("Could not create principal");
        }

        if (principal.Identity is not ClaimsIdentity identity)
        {
            throw new Exception("Could not create identity");
        }

        identity.AddClaim(new Claim("IsAdmin", user.IsAdmin.ToString()));
        return principal;
    }
}
