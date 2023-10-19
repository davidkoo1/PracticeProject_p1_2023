using Microsoft.AspNetCore.Mvc;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class LabWorkController : Controller
    {
        private readonly ILaboratoryRepository _laboratoryRepository;

        public LabWorkController(ILaboratoryRepository laboratoryRepository)
        {
            _laboratoryRepository = laboratoryRepository;
        }
        public IActionResult Create(int courseId, int lessonNumber)
        {
            var lesson = _laboratoryRepository.GetBycourseIdAndlessonIdAsync(courseId, lessonNumber);
            CreateLabWorkViewModel labWorkVM = new CreateLabWorkViewModel()
            {
                //OrderNumber = _dbContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber).Id,
                lessonId = lesson.Id

            };

            return View(labWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLabWorkViewModel labWorkVM)
        {

            if (!ModelState.IsValid)
            {
                return View(labWorkVM);
            }
            var lesson = _laboratoryRepository.GetById(labWorkVM.lessonId);
            LabWork labWork = new LabWork()
            {
                Task = labWorkVM.Task,
                Lesson = _laboratoryRepository.GetById(labWorkVM.lessonId)
            };
            _laboratoryRepository.Add(labWork);
            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });
        }
    }
}
