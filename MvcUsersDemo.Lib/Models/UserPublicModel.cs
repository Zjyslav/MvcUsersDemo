namespace MvcUsersDemo.Lib.Models;
public class UserPublicModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserRole Role { get; set; }

    public UserPublicModel(UserModel user)
    {
        Id = user.Id;
        Name = user.Name;
        Role = user.Role;
    }
}
