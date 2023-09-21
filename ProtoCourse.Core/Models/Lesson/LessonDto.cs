namespace ProtoCourse.Core.Models.Lesson;

public class LessonDto : CreateLessonDto, IBaseDto
{
    public Guid Id { get; set; }
}
