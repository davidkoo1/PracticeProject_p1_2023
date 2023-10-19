using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;

namespace PracticeProject.Repository
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public HomeworkRepository(ApplicationDbContext applicationDbContext)
        {
            _dataContext = applicationDbContext;
        }
        public bool Add(HomeWork homeWork)
        {
            _dataContext.Add(homeWork);
            return Save();
        }

        public bool Delete(HomeWork homeWork)
        {
            _dataContext.Remove(homeWork);
            return Save();
        }

        public Lesson GetBycourseIdAndlessonIdAsync(int courseId, int lessonNumber) => _dataContext.Lessons.FirstOrDefault(x => x.Course.Id == courseId && x.OrderNumber == lessonNumber);

        public Lesson GetById(int id) => _dataContext.Lessons.FirstOrDefault(x => x.Id == id);

        public int GetLastWorkOrderNumberByLessonId(int lessonid)
        {
            var lesson = _dataContext.Lessons.FirstOrDefault(x => x.Id == lessonid);
            return lesson.HomeWork.Count > 0 ? lesson.HomeWork.Max(x => x.OrderNumber) + 1 : lesson.HomeWork.Count + 1;
        }

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(HomeWork homeWork)
        {
            _dataContext.Update(homeWork);
            return Save();
        }
    }
}
