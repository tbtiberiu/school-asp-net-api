using Core.Dtos;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Enums;

namespace Core.Services
{
    public class GradeService
    {
        private readonly UnitOfWork unitOfWork;

        public GradeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public GradeAddDto Add(GradeAddDto payload)
        {
            if (payload == null) return null;

            var newGrade = new Grade
            {
                Value = payload.Value,
                Course = payload.Course,
                DateCreated = payload.DateCreated,

                StudentId = payload.StudentId
            };

            unitOfWork.Grades.Insert(newGrade);
            unitOfWork.SaveChanges();

            return payload;
        }

        public List<GradeExtraDto> GetAll()
        {
            var results = unitOfWork.Grades.GetAll();

            return results;
        }

        public List<Grade> GetGradesByStudentId(int studentId)
        {
            var results = unitOfWork.Grades.GetGradesByStudentId(studentId);

            return results;
        }

        public List<Grade> GetGradesByStudentIdAndCourse(int studentId, CourseType course)
        {
            var results = unitOfWork.Grades.GetGradesByStudentIdAndCourse(studentId, course);

            return results;
        }

        public Dictionary<CourseType, List<Grade>> GetGroupedGradesByStudentId(int studentId)
        {
            var results = unitOfWork.Grades.GetGroupedGradesByStudentId(studentId);

            return results;
        }

        public Dictionary<CourseType, double> GetGroupedGradesAverage(int studentId)
        {
            var results = unitOfWork.Grades.GetGroupedGradesByStudentId(studentId);

            var average = new Dictionary<CourseType, double>();
            foreach (var grade in results)
            {
                var courseAverage = grade.Value.Average(e => e.Value);
                average.Add(grade.Key, courseAverage);
            }

            return average;
        }
    }
}
