using Microsoft.AspNetCore.Mvc;
using MvcUsersDemo.Lib.Models;

namespace MvcUsersDemo.Web.Services;
public interface IIdentityService
{
    Task<bool> LogIn(string username, string password, Controller controller);
    Task LogOut(Controller controller);
    bool RegisterUser(string username, string password, UserRole role);
}