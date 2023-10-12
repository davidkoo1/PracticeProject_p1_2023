using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;

namespace PracticeProject.Controllers
{
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LessonController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index(int courseId, int lessonNumber)
        {
            var lesson = _applicationDbContext.Lessons.FirstOrDefault(x => x.IdCourse == courseId && x.OrderNumber == lessonNumber);
            return View(lesson);
        }
    }
}
