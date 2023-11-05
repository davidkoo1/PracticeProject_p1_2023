using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<User> GetByIdAsyncNoTracking(string id);
        Task<IList<Course>> GetCoursesByGrupaUser(string grupaId);
        Task<IList<User>> GetUsersByGrupaUser(string grupaId);
        Task<string> GetCurrentGrupaByUser(string id);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
