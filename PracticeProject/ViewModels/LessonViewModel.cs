using PracticeProject.Data.Enum;

namespace PracticeProject.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public CourseStatus IsOpen { get; set; }
        public int courseId { get; set; }
    }
}
