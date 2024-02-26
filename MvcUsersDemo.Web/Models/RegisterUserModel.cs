using MvcUsersDemo.Lib.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcUsersDemo.Web.Models;

public class RegisterUserModel
{
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public UserRole Role { get; set; }
}
