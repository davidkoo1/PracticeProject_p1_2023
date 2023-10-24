using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.Repository;
using PracticeProject.Services;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task<IActionResult> Index(int courseId, int lessonNumber)
        {
            //Передаю максимальный номре(перехожу на ласт урок) если номера нет то статус -1 если -1то переход на стр с соданием                                                                          //for me(tommorow): 
            var lesson = await _lessonRepository.GetByCourseAndOrderNumberAsync(courseId, lessonNumber); //1 - big name(course) - norm view, 2 - fix visible info button(js - 1)
            if (lesson != null)
                return View(lesson);

            return NotFound();
        }

        public IActionResult Create(int courseId)
        {
            CreateLessonViewModel lessonVM = new CreateLessonViewModel()
            {
                OrderNumber = _lessonRepository.GetLastLessonNumber(courseId)
            };

            return View(lessonVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonViewModel lessonVM)
        {

            if (!ModelState.IsValid)
            {
                return View(lessonVM);
            }
            Lesson lesson = new Lesson()
            {
                Name = lessonVM.Name,
                OrderNumber = lessonVM.OrderNumber,
                IsOpen = lessonVM.IsOpen,
                Course = _lessonRepository.GetCourseByIdAsync(lessonVM.courseId)
            };
            _lessonRepository.Add(lesson);
            return RedirectToAction("Index", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });
        }

        public async Task<IActionResult> Edit(int id)
        {


            var lesson = await _lessonRepository.GetByIdAsync(id);


            if (lesson == null)
                return View("Error");
            var lessonVM = new EditLessonViewModel
            {
                Name = lesson.Name,
                OrderNumber = lesson.OrderNumber,
                IsOpen = lesson.IsOpen,
                courseId = lesson.Course.Id
            };

            return View(lessonVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditLessonViewModel lessonVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit lesson");
                return View("Edit", lessonVM);
            }



            var lessonEdit = await _lessonRepository.GetByIdAsyncNoTracking(id);
            if (lessonEdit != null)
            {

                var lesson = new Lesson
                {
                    Id = id,
                    Name = lessonVM.Name,
                    IsOpen = lessonVM.IsOpen,
                    OrderNumber = lessonVM.OrderNumber,
                    Course = _lessonRepository.GetCourseByIdAsync(lessonVM.courseId)
                };

                _lessonRepository.Update(lesson);

                return RedirectToAction("Index", new { courseId = lessonVM.courseId, lessonNumber = lessonVM.OrderNumber });
            }

            return View(lessonVM);

        }
    }
}
