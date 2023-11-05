using PracticeProject.Data.Enum;

namespace PracticeProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public CourseStatus IsOpen { get; set; }

        public virtual User? User { get; set; }
        public virtual List<Lesson> Lessons { get; set; }
        public virtual List<CourseGrupa> courseGrupas { get; set; }
        public virtual List<CourseRole>? courseRoles { get; set; }
    }
}
