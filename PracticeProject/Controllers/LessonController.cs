using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;

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
                                                                                                          //for me(tommorow): 
            var lesson = await _lessonRepository.GetByCourseAndOrderNumberAsync(courseId, lessonNumber); //1 - big name(course) - norm view, 2 - fix visible info button(js - 1)
            if (lesson != null)
                return View(lesson);

            return NotFound();
        }
    }
}
