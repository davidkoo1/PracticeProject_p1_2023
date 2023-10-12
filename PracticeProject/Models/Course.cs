namespace PracticeProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public bool IsOpen { get; set; }
        
        public virtual IEnumerable<Lesson> Lessons { get; set; }
        public virtual IEnumerable<CourseGrupa> courseGrupas { get; set; }
    }
}
