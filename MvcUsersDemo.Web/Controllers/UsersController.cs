using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcUsersDemo.Lib.Models;
using MvcUsersDemo.Lib.Servies;
using MvcUsersDemo.Web.Models;
using MvcUsersDemo.Web.Services;
using System.Security.Claims;

namespace MvcUsersDemo.Web.Controllers;
public class UsersController : Controller
{
    private readonly IIdentityService _identityService;

    public UsersController(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            if (ModelState.IsValid && await _identityService.LogIn(model.UserName, model.Password, this))
            {
                TempData["LoginSuccessful"] = $"You are logged in.";
                return RedirectToAction("Index");
            }
        }
        catch
        {

        }
        TempData["LoginUnsuccessful"] = "Could not log in.";
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _identityService.LogOut(this);
        return RedirectToAction("Index");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterUserModel model)
    {
        try
        {
            if (ModelState.IsValid && _identityService.RegisterUser(model.UserName, model.Password, UserRole.Regular))
            {
                return RedirectToAction("Index");
            }
        }
        catch
        {
        }
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
