namespace ProtoCourse.Core.Models.Course;

public class UpdateCourseDto : BaseCourseDto, IBaseDto
{
    public Guid Id { get; set; }
}
