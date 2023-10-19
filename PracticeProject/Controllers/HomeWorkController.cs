using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class HomeWorkController : Controller
    {
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeWorkController(IHomeworkRepository homeworkRepository)
        {
            _homeworkRepository = homeworkRepository;
        }
        public IActionResult Create(int courseId, int lessonNumber)
        {
            var lesson = _homeworkRepository.GetBycourseIdAndlessonIdAsync(courseId, lessonNumber);
            CreateHomeWorkViewModel homeWorkVM = new CreateHomeWorkViewModel()
            {
                //OrderNumber = _dbContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber).Id,
                lessonId = lesson.Id,
                OrderNumber = _homeworkRepository.GetLastWorkOrderNumberByLessonId(lesson.Id)

            };
            
            return View(homeWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHomeWorkViewModel homeWorkVM)
        {

            if (!ModelState.IsValid)
            {
                return View(homeWorkVM);
            }
            var lesson = _homeworkRepository.GetById(homeWorkVM.lessonId);
            HomeWork homeWork = new HomeWork()
            {
                OrderNumber = homeWorkVM.OrderNumber,
                Task = homeWorkVM.Task,
                Lesson = _homeworkRepository.GetById(homeWorkVM.lessonId)
            };
            _homeworkRepository.Add(homeWork);
            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });
        }
    }
}
