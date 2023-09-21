namespace ProtoCourse.Core.Models.Lesson;

public class UpdateLessonDto : BaseLessonDto, IBaseDto
{
    public Guid Id { get; set; }
}
