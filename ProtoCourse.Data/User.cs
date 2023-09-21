using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoCourse.Data;

public class User : IdentityUser
{
    [InverseProperty("Author")]
    public List<Course> TeachingCourses { get; set; }
    [InverseProperty("Students")]
    public List<Course> StudingCourses { get; set; }
}
