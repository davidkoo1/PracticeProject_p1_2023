using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.Repository;
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
            HomeWorkViewModel homeWorkVM = new HomeWorkViewModel()
            {
                //OrderNumber = _dbContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber).Id,
                lessonId = lesson.Id,
                OrderNumber = _homeworkRepository.GetLastWorkOrderNumberByLessonId(lesson.Id)

            };
            
            return View(homeWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeWorkViewModel homeWorkVM)
        {

            if (!ModelState.IsValid)
            {
                return View(homeWorkVM);
            }
            var lesson = _homeworkRepository.GetLessonById(homeWorkVM.lessonId);
            HomeWork homeWork = new HomeWork()
            {
                OrderNumber = homeWorkVM.OrderNumber,
                Task = homeWorkVM.Task,
                Lesson = _homeworkRepository.GetLessonById(homeWorkVM.lessonId)
            };
            _homeworkRepository.Add(homeWork);
            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });
        }



        public async Task<IActionResult> Edit(int id)
        {


            var homeWork = await _homeworkRepository.GetByIdAsync(id);


            if (homeWork == null)
                return View("Error");
            var homeWorkVM = new HomeWorkViewModel
            {
                Id = id,
                Task = homeWork.Task,
                OrderNumber = homeWork.OrderNumber,
                lessonId = homeWork.Lesson.Id
            };

            return View(homeWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HomeWorkViewModel homeWorkVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit text");
                return View("Edit", homeWorkVM);
            }



            var homeworkEdit = await _homeworkRepository.GetByIdAsyncNoTracking(id);
            if (homeworkEdit != null)
            {

                var homework = new HomeWork
                {
                    Id = id,
                    Task = homeWorkVM.Task,
                    OrderNumber = homeWorkVM.OrderNumber,
                    Lesson = _homeworkRepository.GetLessonById(homeWorkVM.lessonId)
                };

                _homeworkRepository.Update(homework);

                return RedirectToAction("Index", "Lesson", new { courseId = homework.Lesson.Course.Id, lessonNumber = homework.Lesson.OrderNumber });
            }

            return View(homeWorkVM);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var homeworkDetails = await _homeworkRepository.GetByIdAsync(id);
            var lesson = homeworkDetails.Lesson;
            if (homeworkDetails == null)
                return View("Error");
            _homeworkRepository.Delete(homeworkDetails);


            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });

        }
    }
}
