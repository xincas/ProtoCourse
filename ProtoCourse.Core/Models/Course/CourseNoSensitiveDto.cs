using ProtoCourse.Core.Models.Lesson;
using ProtoCourse.Core.Models.User;

namespace ProtoCourse.Core.Models.Course;

public class CourseNoSensitiveDto : BaseCourseDto, IBaseDto
{
    public Guid Id { get; set; }
    public UserNoSensitiveDto Author { get; set; }
    public List<LessonNoSensitiveDto> Lessons { get; set; }
    public List<UserNoSensitiveDto> Students { get; set; }
}
