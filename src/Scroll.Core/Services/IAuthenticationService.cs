using Microsoft.AspNetCore.Identity;
using Scroll.Library.Models.Entities;

namespace Scroll.Core.Services;

public interface IAuthenticationService
{
    Task<bool> LoginAsync(string username, string password);
    Task LogoutAsync();
    Task<bool> RegisterAsync(string username, string password);
    // Other methods and properties related to authentication
}

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<AppUser> signInManager;

    public AuthenticationService(SignInManager<AppUser> signInManager)
    {
        this.signInManager = signInManager;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var result = await signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterAsync(string username, string password)
    {
        var user = new AppUser { UserName = username };
        var result = await signInManager.UserManager.CreateAsync(user, password);
        return result.Succeeded;
    }
}