using Caltec.StudentInfoProject.Domain;
using Microsoft.EntityFrameworkCore;

namespace Caltec.StudentInfoProject.Persistence
{
    public class StudentInfoDbContext : DbContext
    {
        public StudentInfoDbContext()
        {
            
        }
        public StudentInfoDbContext(DbContextOptions<StudentInfoDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Degree> Degrees { get; set; } = null!;
        public DbSet<StudentClass> StudentClasses { get; set; } = null!;
        public DbSet<SchoolFees> SchoolFees { get; set; } = null!;

    }
   
}
