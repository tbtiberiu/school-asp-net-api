using DataLayer.Enums;

namespace DataLayer.Entities;

public class Grade : BaseEntity
{
    public double Value { get; set; }
    public CourseType Course { get; set; }
    public DateTime DateCreated { get; set; }

    public int StudentId { get; set; }
}
