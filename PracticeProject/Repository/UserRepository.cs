using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using System.Collections.Immutable;

namespace PracticeProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public UserRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(User user)
        {
            _dataContext.Add(user);
            return Save();
        }

        public bool Delete(User user)
        {
            _dataContext.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<User>> GetAllUsers() => await _dataContext.Users.ToListAsync();

        public async Task<IList<Course>> GetCoursesByGrupaUser(string grupaId) => await _dataContext.CourseGrupas.Where(x => x.IdGrupa == grupaId).Select(c => c.Course).ToListAsync();

        public async Task<string> GetCurrentGrupaByUser(string id)
        {
            var user = await GetUserById(id);
            return user.GrupaId;
        }


        public async Task<User> GetUserById(string id) => await _dataContext.Users.FindAsync(id);

        public async Task<IList<User>> GetUsersByGrupaUser(string grupaId) => await _dataContext.Users.Where(x => x.GrupaId == grupaId).ToListAsync();

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;
        public bool Update(User user)
        {
            _dataContext.Update(user);
            return Save();
        }
    }
}
