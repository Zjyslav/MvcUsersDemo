using Microsoft.AspNetCore.Identity;
using MvcUsersDemo.Lib.Models;

namespace MvcUsersDemo.Lib.Servies;
public class UserService : IUserService
{
    private readonly List<UserModel> _users = [
        new UserModel()
        {
            Id = 1,
            Name = "Zjyslav",
            // Password = Moose
            PasswordHash = "AQAAAAIAAYagAAAAEFfw9MvpjLrZJxUEhujZ/hDgS505Kje1NuJwAA1dq3pUctzS9TI3HExBMr1q1Wv46g==",
            Role = UserRole.Admin
        },
        new UserModel()
        {
            Id = 2,
            Name = "Adam",
            // Password = regular
            PasswordHash = "AQAAAAIAAYagAAAAEEjSvrczZ0s1gZ1R1eZCBEOwu6sobhwm5SA/E2U1ti4tPwb281R4SlXiaKbntUPnlw==",
            Role = UserRole.Regular
        }];

    private readonly PasswordHasher<UserModel> _hasher = new PasswordHasher<UserModel>();
    public UserPublicModel? LogIn(string username, string password)
    {
        UserModel? user = _users
            .FirstOrDefault(u =>
                u.Name == username);

        if (user is null)
            return null;

        var result = _hasher
            .VerifyHashedPassword(user,
                                  user.PasswordHash,
                                  password);

        if (result == PasswordVerificationResult.Success)
            return new UserPublicModel(user);

        return null;
    }

    public bool RegisterUser(string username, string password, UserRole role)
    {
        if (_users.Any(u => u.Name == username))
            return false;

        UserModel user = new()
        {
            Id = _users.Max(u => u.Id) + 1,
            Name = username,
            Role = role
        };
        user.PasswordHash = _hasher.HashPassword(user, password);
        _users.Add(user);
        return true;
    }
}
