using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface IHomeworkRepository
    {
        Task<HomeWork> GetByIdAsync(int id);
        Task<HomeWork> GetByIdAsyncNoTracking(int id);
        Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber);
        Lesson GetLessonById(int id);
        int GetLastWorkOrderNumberByLessonId(int lessonid);
        bool Add(HomeWork homeWork);
        bool Update(HomeWork homeWork);
        bool Delete(HomeWork homeWork);
        bool Save();
    }
}
