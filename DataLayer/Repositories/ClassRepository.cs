using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ClassRepository : RepositoryBase<Class>
    {
        public ClassRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public List<Class> GetAllWithStudentCount()
        {
            return GetRecords()
                .Include(c => c.Students)
                .Select(c => new Class
                {
                    Id = c.Id,
                    Name = c.Name,
                    StudentCount = c.Students.Count

                }).ToList();
        }

        // get the number of students with grades higher than 5 for each class as procentage
        public Dictionary<int, double> GetStudentPassProcentage()
        {
            return GetRecords()
                .Include(c => c.Students)
                .ThenInclude(s => s.Grades)
                .Select(c => new
                {
                    ClassId = c.Id,
                    StudentCount = c.Students
                        .Where(s => s.Grades.Any(g => g.Value >= 5))
                        .Count(),
                    TotalStudentCount = c.Students.Count
                })
                .ToDictionary(e => e.ClassId, e => (double)e.StudentCount / e.TotalStudentCount);
        }
    }
}
