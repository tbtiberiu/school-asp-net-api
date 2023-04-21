using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Enums;

namespace DataLayer.Repositories
{
    public class GradesRepository : RepositoryBase<Grade>
    {
        private readonly AppDbContext dbContext;

        public GradesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public new List<GradeExtraDto> GetAll()
        {
            var result = dbContext.Grades
                .Join(dbContext.Students,
                    grade => grade.StudentId,
                    student => student.Id,
                    (grade, student) => new GradeExtraDto
                    {
                        Id = grade.Id,
                        Course = grade.Course,
                        DateCreated = grade.DateCreated,
                        StudentId = grade.StudentId,
                        StudentFirstName = student.FirstName,
                        StudentLastName = student.LastName,
                        Value = grade.Value,
                        ClassName = student.Class.Name
                    })
                .ToList();
            return result;
        }


        public List<Grade> GetGradesByStudentId(int studentId)
        {
            var result = dbContext.Grades
                .Where(e => e.StudentId == studentId)
                .ToList();
            return result;
        }

        public List<Grade> GetGradesByStudentIdAndCourse(int studentId, CourseType course)
        {
            var result = dbContext.Grades
                .Where(e => e.StudentId == studentId && e.Course == course)
                .OrderBy(e => e.DateCreated)
                .ToList();
            return result;
        }

        public Dictionary<CourseType, List<Grade>> GetGroupedGradesByStudentId(int studentId)
        {
            var results = dbContext.Grades
                .Where(e => e.StudentId == studentId)
                .GroupBy(e => e.Course)
                .Select(e => new { Course = e.Key, Grades = e.ToList() })
                .ToDictionary(e => e.Course, e => e.Grades);
            return results;
        }
    }
}
