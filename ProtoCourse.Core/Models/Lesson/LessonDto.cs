using ProtoCourse.Core.Models.Course;

namespace ProtoCourse.Core.Models.Lesson;

public class LessonDto : CreateLessonDto, IBaseDto
{
    public Guid Id { get; set; }
    public CourseNoSensitiveDto Course { get; set; }
}
