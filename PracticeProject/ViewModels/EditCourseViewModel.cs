using PracticeProject.Data.Enum;
using PracticeProject.Models;

namespace PracticeProject.ViewModels
{
    public class EditCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public CourseStatus IsOpen { get; set; }
        public virtual List<CourseGrupa>? CourseGrupas { get; set; }
    }
}
