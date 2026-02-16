using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Caltec.StudentInfoProject.Persistence
{
    internal class StudentInfoDbContextFactory: IDesignTimeDbContextFactory<StudentInfoDbContext>
    {
        public StudentInfoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentInfoDbContext>();
            
            string connexionString = @"";
            optionsBuilder.UseSqlServer(connexionString);

            return new StudentInfoDbContext(optionsBuilder.Options);
        }
    }
}
