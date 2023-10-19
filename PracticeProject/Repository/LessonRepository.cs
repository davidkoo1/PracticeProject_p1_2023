using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;

namespace PracticeProject.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public LessonRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool Add(Lesson lesson)
        {
            _dataContext.Add(lesson);
            return Save();
        }

        public bool Delete(Lesson lesson)
        {
            _dataContext.Remove(lesson);
            return Save();
        }

        public async Task<IList<Lesson>> GetAllLessons() => await _dataContext.Lessons.ToListAsync();

        public async Task<Lesson> GetByCourseAndOrderNumberAsync(int courseId, int lessonNumber) => await _dataContext.Lessons.FirstOrDefaultAsync(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber);

        public Course GetCourseByIdAsync(int id) => _dataContext.Courses.FirstOrDefault(x => x.Id == id);
        public async Task<Lesson> GetByIdAsyncNoTraking(int id) => await _dataContext.Lessons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public int GetLastLessonNumber(int id)
        {
            var course = _dataContext.Courses.FirstOrDefault(x => x.Id == id);
            return course.Lessons.Count > 0 ? course.Lessons.Max(x => x.OrderNumber) + 1 : course.Lessons.Count + 1;
        }

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(Lesson lesson)
        {
            _dataContext.Update(lesson);
            return Save();
        }
    }
}
