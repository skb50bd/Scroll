using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using Humanizer;
using LanguageExt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scroll.Core.Extensions;
using Scroll.Core.Services;
using Scroll.Core.Validators;
using Scroll.Domain.DTOs;
using Scroll.Domain.Exceptions;
using Scroll.Domain.InputModels;

namespace Scroll.Api.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> Get() =>
        await _userService.GetCurrentUser() is { } user
            ? Ok(user)
            : Unauthorized();

    [HttpPost("Register")]
    public Task<ActionResult<UserDto>> Register(UserRegistrationModel model) =>
        _userService
            .Register(model)
            .MatchAsync(
                user => CreatedAtAction(nameof(Get), user),
                exception => exception switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    IdentityException ex   => UnprocessableEntity(ex.ToProblemDetails()),
                    _                      => throw StandardErrors.Unreachable
                }
            );

    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginModel login)
    {
        var loginResult = await _userService.Login(login);
        var result = await loginResult.MapAsync(async userDto =>
        {
            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[]
                        {
                            new(ClaimTypes.NameIdentifier, userDto.UserName),
                            new(ClaimTypes.Name, userDto.FullName),
                            new(ClaimTypes.Email, userDto.Email)
                        },
                        CookieAuthenticationDefaults.AuthenticationScheme
                    )));

            return Ok();
        });

        return result.Match(
            res => Ok() as ActionResult,
            res => res switch
            {
                ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                InvalidCredentials => Unauthorized("Invalid credentials"),
                _                  => throw StandardErrors.Unreachable
            }
        );
    }

    [HttpPost("Token")]
    public Task<ActionResult<string>> GetToken(LoginModel model) =>
        _userService
            .CreateAccessToken(model)
            .MatchAsync<JwtSecurityToken, string>(
                token =>
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenString  = tokenHandler.WriteToken(token);
                    return Ok(tokenString);
                },
                exception => exception switch
                {
                    ValidationException ex => UnprocessableEntity(ex.ToProblemDetails()),
                    InvalidCredentials     => Unauthorized("Invalid credentials"),
                    _                      => throw StandardErrors.Unreachable
                }
            );
}