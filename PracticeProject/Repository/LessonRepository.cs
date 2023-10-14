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
            throw new NotImplementedException();
        }

        public bool Delete(Lesson lesson)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Lesson>> GetAllLessons()
        {
            throw new NotImplementedException();
        }

        public async Task<Lesson> GetByCourseAndOrderNumberAsync(int courseId, int lessonNumber) => await _dataContext.Lessons.FirstOrDefaultAsync(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber);

        public Task<Lesson> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Lesson> GetByIdAsyncNoTraking(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Lesson lesson)
        {
            throw new NotImplementedException();
        }
    }
}
