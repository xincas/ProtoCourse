using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProtoCourse.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasOne(c => c.Author)
            .WithMany(u => u.TeachingCourses)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.Students)
            .WithMany(u => u.StudingCourses)
            .UsingEntity(
                "CourseStudent",
                l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UsersId").OnDelete(DeleteBehavior.NoAction).HasPrincipalKey(nameof(User.Id)),
                r => r.HasOne(typeof(Course)).WithMany().HasForeignKey("CoursesId").OnDelete(DeleteBehavior.NoAction).HasPrincipalKey(nameof(Course.Id)),
                j => j.HasKey("UsersId", "CoursesId"));
    }
}
