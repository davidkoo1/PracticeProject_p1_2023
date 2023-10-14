using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface ILessonRepository
    {
        Task<IList<Lesson>> GetAllLessons();
        Task<Lesson> GetByIdAsync(int id);
        Task<Lesson> GetByCourseAndOrderNumberAsync(int courseId, int lessonNumber);
        Task<Lesson> GetByIdAsyncNoTraking(int id);
        bool Add(Lesson lesson);
        bool Update(Lesson lesson);
        bool Delete(Lesson lesson);
        bool Save();
    }
}
