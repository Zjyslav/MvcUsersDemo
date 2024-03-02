using Microsoft.AspNetCore.Mvc;
using MvcUsersDemo.Lib.Models;

namespace MvcUsersDemo.Web.Services;
public interface IIdentityService
{
    Task<bool> LogIn(string username, string password);
    Task LogOut();
    bool RegisterUser(string username, string password, UserRole role);
}