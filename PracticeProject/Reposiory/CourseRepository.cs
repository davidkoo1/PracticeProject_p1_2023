using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interfaces;
using PracticeProject.Models;

namespace PracticeProject.Reposiory
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public CourseRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool Add(Course course)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Course course)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Course>> GetAllCourse() => await _dataContext.Courses.ToListAsync();

        public Task<Course> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetByIdAsyncNoTraking(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
