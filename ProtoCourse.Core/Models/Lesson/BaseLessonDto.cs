using System.ComponentModel.DataAnnotations;

namespace ProtoCourse.Core.Models.Lesson;

public abstract class BaseLessonDto
{
    [Required]
    public int Number { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public string VideoUrl { get; set; }
}
