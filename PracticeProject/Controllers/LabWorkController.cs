using Microsoft.AspNetCore.Mvc;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.Repository;
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
            LabWorkViewModel labWorkVM = new LabWorkViewModel()
            {
                //OrderNumber = _dbContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber).Id,
                lessonId = lesson.Id

            };

            return View(labWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LabWorkViewModel labWorkVM)
        {

            if (!ModelState.IsValid)
            {
                return View(labWorkVM);
            }
            var lesson = _laboratoryRepository.GetLessonById(labWorkVM.lessonId);
            LabWork labWork = new LabWork()
            {
                Task = labWorkVM.Task,
                Lesson = _laboratoryRepository.GetLessonById(labWorkVM.lessonId)
            };
            _laboratoryRepository.Add(labWork);
            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });
        }

        public async Task<IActionResult> Edit(int id)
        {


            var labWork = await _laboratoryRepository.GetByIdAsync(id);


            if (labWork == null)
                return View("Error");
            var labWorkVM = new LabWorkViewModel
            {
                Id = id,
                Task = labWork.Task,
                lessonId = labWork.Lesson.Id
            };

            return View(labWorkVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LabWorkViewModel labWorkVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit text");
                return View("Edit", labWorkVM);
            }



            var labWorkEdit = await _laboratoryRepository.GetByIdAsyncNoTracking(id);
            if (labWorkEdit != null)
            {

                var labWork = new LabWork
                {
                    Id = id,
                    Task = labWorkVM.Task,
                    Lesson = _laboratoryRepository.GetLessonById(labWorkVM.lessonId)
                };

                _laboratoryRepository.Update(labWork);

                return RedirectToAction("Index", "Lesson", new { courseId = labWork.Lesson.Course.Id, lessonNumber = labWork.Lesson.OrderNumber });
            }

            return View(labWorkVM);

        }
        public async Task<IActionResult> Delete(int id)
        {
            var labworkDetails = await _laboratoryRepository.GetByIdAsync(id);
            var lesson = labworkDetails.Lesson;
            if (labworkDetails == null)
                return View("Error");
            _laboratoryRepository.Delete(labworkDetails);


            return RedirectToAction("Index", "Lesson", new { courseId = lesson.Course.Id, lessonNumber = lesson.OrderNumber });

        }
    }
}
