using Microsoft.AspNetCore.Identity;
using MvcUsersDemo.Lib.Models;

namespace MvcUsersDemo.Lib.Servies;

public interface IUserService
{
    UserPublicModel? LogIn(string username, string password);
    bool RegisterUser(string username, string password, UserRole role);
}