namespace MvcUsersDemo.Lib.Models;
public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    Regular,
    Admin
}