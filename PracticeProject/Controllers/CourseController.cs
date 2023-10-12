using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Interface;

namespace PracticeProject.Controllers
{
    public class CourseController : Controller
    {
        
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllCourse();
            return View(courses);
        }

        
        public async Task<IActionResult> About(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return View(course);
        }
    }
}
