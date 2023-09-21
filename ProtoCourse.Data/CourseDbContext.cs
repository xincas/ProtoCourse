using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProtoCourse.Data.Configurations;

namespace ProtoCourse.Data;

public class CourseDbContext : IdentityDbContext<User>
{
    public CourseDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new CourseConfiguration());
        builder.ApplyConfiguration(new LessonConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        base.OnModelCreating(builder);
    }
}

public class HotelListingDbContextFactory : IDesignTimeDbContextFactory<CourseDbContext>
{
    public CourseDbContext CreateDbContext(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
        var conn = config.GetConnectionString("CourseDbConnectionString");
        optionsBuilder.UseSqlServer(conn);
        return new CourseDbContext(optionsBuilder.Options);
    }
}
