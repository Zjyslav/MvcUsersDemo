using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MvcUsersDemo.Lib.Servies;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MvcUsersDemo.Lib.Models;

namespace MvcUsersDemo.Web.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserService _userService;

    public IdentityService(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<bool> LogIn(string username, string password, Controller controller)
    {
        var hasher = new PasswordHasher<string>();
        string passwordHash = hasher.HashPassword(username, password);

        var user = _userService.LogIn(username, password);
        if (user is null)
            return false;

        var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties()
        {
            AllowRefresh = true,
            IsPersistent = true,
        };

        await controller.HttpContext.SignInAsync("CookieAuth",
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return true;
    }

    public async Task LogOut(Controller controller)
    {
        await controller.HttpContext.SignOutAsync("CookieAuth");
    }

    public bool RegisterUser(string username, string password, UserRole role)
    {
        return _userService.RegisterUser(username, password, role);
    }
}
