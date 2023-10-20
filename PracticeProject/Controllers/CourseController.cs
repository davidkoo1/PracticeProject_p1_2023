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
        private readonly IPhotoService _photoservice;

        public CourseController(ICourseRepository courseRepository, IPhotoService photoService)
        {
            _courseRepository = courseRepository;
            _photoservice = photoService;
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
            if (ModelState.IsValid)
            {
                var result = await _photoservice.AddPhotoAsync(courseVM.Image);


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
                    Image = result.Url.ToString(),
                    courseGrupas = courseVM.CourseGrupas
                };
                
                _courseRepository.Add(course);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(courseVM);

        }

    }
}
