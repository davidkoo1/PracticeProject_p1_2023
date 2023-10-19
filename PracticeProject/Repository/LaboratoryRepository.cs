using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;

namespace PracticeProject.Repository
{
    public class LaboratoryRepository : ILaboratoryRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public LaboratoryRepository(ApplicationDbContext applicationDbContext)
        {
            _dataContext = applicationDbContext;
        }
        public bool Add(LabWork labWork)
        {
            _dataContext.Add(labWork);
            return Save();
        }

        public bool Delete(LabWork labWork)
        {
            _dataContext.Remove(labWork);
            return Save();
        }

        public Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber) => _dataContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber);

        public Lesson GetById(int id) => _dataContext.Lessons.FirstOrDefault(x => x.Id == id);


        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(LabWork labWork)
        {
            _dataContext.Update(labWork);
            return Save();
        }
    }
}
