using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface ILaboratoryRepository
    {
        Task<LabWork> GetByIdAsync(int id);
        Task<LabWork> GetByIdAsyncNoTracking(int id);
        Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber);
        Lesson GetLessonById(int id);
        bool Add(LabWork labWork);
        bool Update(LabWork labWork);
        bool Delete(LabWork labWork);
        bool Save();
    }
}
