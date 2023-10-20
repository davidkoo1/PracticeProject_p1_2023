using PracticeProject.Data.Enum;
using PracticeProject.Models;

namespace PracticeProject.ViewModels
{
    public class CreateCourseViewModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public CourseStatus IsOpen { get; set; }

       // public CreateLessonViewModel? LessonVM { get; set; }
        public virtual List<CourseGrupa>? CourseGrupas { get; set; }
    }
}
