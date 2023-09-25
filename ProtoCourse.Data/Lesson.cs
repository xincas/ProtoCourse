using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoCourse.Data;

public class Lesson : BaseEntity
{
    [ForeignKey(nameof(Course))]
    public Guid CourseId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string VideoUrl { get; set; }
    public Course Course { get; set; }
}
