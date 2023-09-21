using System.ComponentModel.DataAnnotations;

namespace ProtoCourse.Core.Models.Lesson;

public class CreateLessonDto : BaseLessonDto
{
    [Required]
    public Guid CourseId { get; set; }
}
