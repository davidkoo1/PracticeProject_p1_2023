using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;

namespace PracticeProject.Repository
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

        public async Task<Course> GetByIdAsync(int id) => await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == id);

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
