using System.ComponentModel.DataAnnotations;

namespace ProtoCourse.Core.Models.User;

public class UserNoSensitiveDto
{
    public string Id { get; set; }
    [Required]
    public string Username { get; set; }

}
