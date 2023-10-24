using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface ITextRepository
    {
        Task<TextWork> GetByIdAsync(int id);
        Task<TextWork> GetByIdAsyncNoTracking(int id);
        Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber);
        Lesson GetLessonById(int id);
        int GetLastTextWorkOrderNumberByLessonId(int lessonid);
        bool Add(TextWork text);
        bool Update(TextWork text);
        bool Delete(TextWork text);
        bool Save();
    }
}
