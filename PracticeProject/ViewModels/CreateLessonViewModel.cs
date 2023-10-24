using PracticeProject.Data.Enum;

namespace PracticeProject.ViewModels
{
    public class CreateLessonViewModel
    {
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public CourseStatus IsOpen { get; set; }
        public int courseId { get; set; }
    }
}
