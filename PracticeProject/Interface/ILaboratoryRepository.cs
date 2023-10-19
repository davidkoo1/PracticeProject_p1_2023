using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface ILaboratoryRepository
    {
        Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber);
        Lesson GetById(int id);
        bool Add(LabWork labWork);
        bool Update(LabWork labWork);
        bool Delete(LabWork labWork);
        bool Save();
    }
}
