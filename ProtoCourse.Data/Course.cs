using System.ComponentModel.DataAnnotations.Schema;

namespace ProtoCourse.Data;

public class Course
{
    public Guid Id { get; set; }
    [ForeignKey(nameof(Author))]
    public string AuthorId;
    public string Title { get; set; }
    public string Description { get; set; }
    public string HeaderImageUrl { get; set; }
    public User Author { get; set; }
    public List<Lesson?> Lessons { get; set; }

    //public List<string> StudentIds { get => Students.Select(s => s.Id).ToList(); }
    public List<User> Students { get; set; } = new();
}
