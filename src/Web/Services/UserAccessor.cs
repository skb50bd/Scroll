using Microsoft.AspNetCore.Identity;
using Scroll.Domain.Entities;
using Scroll.Web.Identity;

namespace Scroll.Web.Services;

internal sealed class UserAccessor(
    IHttpContextAccessor httpContextAccessor,
    UserManager<User> userManager,
    IdentityRedirectManager redirectManager)
{
    public async Task<User> GetRequiredUserAsync()
    {
        var principal =
            httpContextAccessor.HttpContext?.User
            ?? throw new InvalidOperationException(
                    $"{nameof(GetRequiredUserAsync)} requires access to an {nameof(HttpContext)}."
            );

        var user = await userManager.GetUserAsync(principal);

        if (user is null)
        {
            // Throws NavigationException, which is handled by the framework as a redirect.
            redirectManager.RedirectToWithStatus(
                "/Account/InvalidUser",
                "Error: Unable to load user with ID '{userManager.GetUserId(principal)}'.");
        }

        return user;
    }
}