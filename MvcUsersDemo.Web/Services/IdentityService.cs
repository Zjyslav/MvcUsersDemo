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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<bool> LogIn(string username, string password)
    {
        var user = _userService.LogIn(username, password);
        if (user is null)
            return false;

        var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

        var authProperties = new AuthenticationProperties()
        {
            AllowRefresh = true,
            IsPersistent = true,
        };

        if (_httpContextAccessor.HttpContext is null)
            return false;

        await _httpContextAccessor.HttpContext.SignInAsync("CookieAuth",
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return true;
    }

    public async Task LogOut()
    {
        if (_httpContextAccessor.HttpContext is null)
            return;
        await _httpContextAccessor.HttpContext.SignOutAsync("CookieAuth");
    }

    public bool RegisterUser(string username, string password, UserRole role)
    {
        return _userService.RegisterUser(username, password, role);
    }
}
