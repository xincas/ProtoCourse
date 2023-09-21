namespace ProtoCourse.Core.Models.Course;

public class CreateCourseDto : BaseCourseDto
{
    public Guid AuthorId { get; set; }
}
