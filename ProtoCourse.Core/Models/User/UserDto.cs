using System.ComponentModel.DataAnnotations;

namespace ProtoCourse.Core.Models.User;

public class UserDto : LoginDto
{
    [Required]
    public string UserName { get; set; }
}
