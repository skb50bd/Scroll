﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Scroll.Library.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace Scroll.Web.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserStore<AppUser> _userStore;
    private readonly IUserEmailStore<AppUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;

    public RegisterModel(
        UserManager<AppUser> userManager,
        IUserStore<AppUser> userStore,
        SignInManager<AppUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender)
    {
        _userManager   = userManager;
        _userStore     = userStore;
        _emailStore    = GetEmailStore();
        _signInManager = signInManager;
        _logger        = logger;
        _emailSender   = emailSender;
    }

    [BindProperty]
    public InputModel? Input { get; set; }

    public string? ReturnUrl { get; set; }

    //public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "Full Name")]
        [StringLength(
            maximumLength: 200,
            MinimumLength = 1,
            ErrorMessage = "Full Name must be between 1 and 200 characters.")]
        public string? FullName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
        //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        await Task.CompletedTask;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid is false)
        {
            return Page();
        }

        var user = new AppUser();

        await _userStore
            .SetUserNameAsync(
                user,
                Input!.Username,
                CancellationToken.None);

        await _emailStore
            .SetEmailAsync(
                user,
                Input.Email,
                CancellationToken.None);

        user.FullName = Input.FullName!;

        var result =
            await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password.");

            var userId =
                await _userManager.GetUserIdAsync(user);

            var code =
                await _userManager
                    .GenerateEmailConfirmationTokenAsync(user);

            code =
                WebEncoders.Base64UrlEncode(
                    Encoding.UTF8.GetBytes(code));

            var callbackUrl =
                Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId, code, returnUrl },
                    protocol: Request.Scheme);

            await _emailSender
                .SendEmailAsync(
                    Input.Email,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>clicking here</a>.");

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return RedirectToPage(
                    "RegisterConfirmation",
                    new
                    {
                        email = Input.Email,
                        returnUrl
                    });
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(
                string.Empty,
                error.Description);
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }

    private IUserEmailStore<AppUser> GetEmailStore()
    {
        if (_userManager.SupportsUserEmail is false)
        {
            throw new NotSupportedException(
                "The default UI requires a user store with email support.");
        }

        return (IUserEmailStore<AppUser>)_userStore;
    }
}
