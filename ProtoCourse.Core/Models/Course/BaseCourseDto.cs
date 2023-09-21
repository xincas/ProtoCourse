using System.ComponentModel.DataAnnotations;

namespace ProtoCourse.Core.Models.Course;

public abstract class BaseCourseDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    [MaxLength(512)]
    public string Description { get; set; }
    [Required]
    [MaxLength(128)]
    public string HeaderImageUrl { get; set; }
}
