using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Data.Enum;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.ViewModels;

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

        public IActionResult Create()
        {
            var grups = _courseRepository.GetAllGrups();
            ViewBag.Grups = grups;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel courseVM, string[] selectedGrups)
        {
            /* if(!ModelState.IsValid)
             {
                 return View(course);
             }
            */
            courseVM.CourseGrupas = new List<CourseGrupa>();
            for (int i = 0; i < selectedGrups.Length; i++)
            {
                CourseGrupa tmp = new CourseGrupa()
                {
                    IdGrupa = selectedGrups[i]
                };
                courseVM.CourseGrupas.Add(tmp);
            }

            Course course = new Course()
            {
                Name = courseVM.Name,
                IsOpen = courseVM.IsOpen,
                Image = "Defaultsrc",
                courseGrupas = courseVM.CourseGrupas
            };
/*            if (courseVM.LessonVM == null)
                course.IsOpen = CourseStatus.Close;
            else
                course.Lessons = new List<Lesson>()
                {
                    new Lesson()
                    {
                        Name = courseVM.LessonVM.Name,
                        OrderNumber = 1,
                        IsOpen = true,
                        Course = course
                    }
                };*/

            _courseRepository.Add(course);
            return RedirectToAction("Index");
        }

    }
}
