using DataLayer.Enums;

namespace DataLayer.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public CourseType Course { get; set; }
        public DateTime DateCreated { get; set; }
        public int StudentId { get; set; }
    }
}
