namespace ProtoCourse.Core.Models.Course;

public class GetCourseDto : BaseCourseDto, IBaseDto
{
    public Guid Id { get; set; }
}
