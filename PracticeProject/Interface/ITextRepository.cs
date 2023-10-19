using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface ITextRepository
    {
        Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber);
        Lesson GetById(int id);
        int GetLastTextWorkOrderNumberByLessonId(int lessonid);
        bool Add(TextWork text);
        bool Update(TextWork text);
        bool Delete(TextWork text);
        bool Save();
    }
}
