using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class TextWorkController : Controller
    {
        private readonly ITextRepository _textRepository;

        public TextWorkController(ITextRepository textRepository)
        {
            _textRepository = textRepository;
        }
        public IActionResult Create(int courseId, int lessonNumber)
        {
            var lesson = _textRepository.GetBycourseIdAndlessonIdAsync(courseId, lessonNumber);
            CreateTextWorkViewModel textWorkVM = new CreateTextWorkViewModel()
            {
                //OrderNumber = _dbContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber).Id,
                lessonId = lesson.Id,
                OrderNumber = _textRepository.GetLastTextWorkOrderNumberByLessonId(lesson.Id)

            };
            
            return View(textWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTextWorkViewModel textWorkVM)
        {

            if (!ModelState.IsValid)
            {
                return View(textWorkVM);
            }
            var lesson = _textRepository.GetById(textWorkVM.lessonId);
            TextWork textWork = new TextWork()
            {
                OrderNumber = textWorkVM.OrderNumber,
                Task = textWorkVM.Task,
                Lesson = _textRepository.GetById(textWorkVM.lessonId)
            };
            _textRepository.Add(textWork);
            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });
        }
    }
}
