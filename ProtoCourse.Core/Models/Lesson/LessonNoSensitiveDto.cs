namespace ProtoCourse.Core.Models.Lesson;

public class LessonNoSensitiveDto : IBaseDto
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
}
