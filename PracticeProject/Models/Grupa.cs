namespace PracticeProject.Models
{
    public class Grupa
    {
        public int Code { get; set; }
        public string FullName { get; set; }

        public virtual IEnumerable<CourseGrupa> courseGrupas { get; set; }
    }
}
