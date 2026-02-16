using Caltec.StudentInfoProject.Domain;
using Caltec.StudentInfoProject.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Caltec.StudentInfoProject.Business
{
    public class DegreeService : BaseService
    {
        public DegreeService(StudentInfoDbContext studentInfoDbContext) : base(studentInfoDbContext)
        {
        }

        public Task<List<Degree>> GetAllAsync(CancellationToken cancellationToken)
        {
            return StudentInfoDbContext.Degrees.ToListAsync(cancellationToken);
        }
    }
}
