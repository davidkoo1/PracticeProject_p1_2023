using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.Repository;
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
            TextWorkViewModel textWorkVM = new TextWorkViewModel()
            {
                //OrderNumber = _dbContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber).Id,
                lessonId = lesson.Id,
                OrderNumber = _textRepository.GetLastTextWorkOrderNumberByLessonId(lesson.Id)

            };
            
            return View(textWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TextWorkViewModel textWorkVM)
        {

            if (!ModelState.IsValid)
            {
                return View(textWorkVM);
            }
            var lesson = _textRepository.GetLessonById(textWorkVM.lessonId);
            TextWork textWork = new TextWork()
            {
                OrderNumber = textWorkVM.OrderNumber,
                Task = textWorkVM.Task,
                Lesson = _textRepository.GetLessonById(textWorkVM.lessonId)
            };
            _textRepository.Add(textWork);
            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });
        }


        public async Task<IActionResult> Edit(int id)
        {


            var textWork = await _textRepository.GetByIdAsync(id);


            if (textWork == null)
                return View("Error");
            var textVM = new TextWorkViewModel
            {
                Id = id,
                Task = textWork.Task,
                OrderNumber = textWork.OrderNumber,
                lessonId = textWork.Lesson.Id
            };

            return View(textVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TextWorkViewModel textVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit text");
                return View("Edit", textVM);
            }



            var textEdit = await _textRepository.GetByIdAsyncNoTracking(id);
            if (textEdit != null)
            {

                var text = new TextWork
                {
                    Id = id,
                    Task = textVM.Task,
                    OrderNumber = textVM.OrderNumber,
                    Lesson = _textRepository.GetLessonById(textVM.lessonId)
                };

                _textRepository.Update(text);

                return RedirectToAction("Index", "Lesson", new { courseId = text.Lesson.Course.Id, lessonNumber = text.Lesson.OrderNumber });
            }

            return View(textVM);

        }
    }

}
