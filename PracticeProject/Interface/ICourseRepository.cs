using PracticeProject.Models;
using System.ComponentModel;

namespace PracticeProject.Interface
{
    public interface ICourseRepository
    {

        Task<IList<Course>> GetAllCourseByUserGrupa();
        Task<IList<Course>> GetAllCourseByUser();

        Task<IList<Course>> GetAllCourse();
        Task<Grupa> GetGrupaById(string id);
        IList<Grupa> GetAllGrups();
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdAsyncNoTracking(int id);

        Task<User> GetUser(string id);
        bool Add(Course course);
        bool Update(Course course);
        bool Delete(Course course);
        bool Save();
    }
}
