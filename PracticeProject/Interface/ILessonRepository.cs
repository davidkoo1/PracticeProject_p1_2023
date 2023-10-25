using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface ILessonRepository
    {
        Task<IList<Lesson>> GetAllLessons();
        Course GetCourseByIdAsync(int id);
        Task<Lesson> GetByCourseAndOrderNumberAsync(int courseId, int lessonNumber);
        Task<Lesson> GetByIdAsyncNoTracking(int id);
        Task<Lesson> GetByIdAsync(int id);
        Task<Lesson> FindFirst(int courseId);

        int GetLastLessonNumber(int id);

        bool Add(Lesson lesson);
        bool Update(Lesson lesson);
        bool Delete(Lesson lesson);
        bool Save();
    }
}
