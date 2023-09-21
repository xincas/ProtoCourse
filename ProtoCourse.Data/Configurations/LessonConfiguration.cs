using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProtoCourse.Data.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
