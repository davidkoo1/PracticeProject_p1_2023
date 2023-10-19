using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface IHomeworkRepository
    {
        Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber);
        Lesson GetById(int id);
        int GetLastWorkOrderNumberByLessonId(int lessonid);
        bool Add(HomeWork homeWork);
        bool Update(HomeWork homeWork);
        bool Delete(HomeWork homeWork);
        bool Save();
    }
}
