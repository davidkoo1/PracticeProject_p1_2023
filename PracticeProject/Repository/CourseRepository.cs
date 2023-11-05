using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Models;
using System.Xml.Linq;

namespace PracticeProject.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public CourseRepository(ApplicationDbContext dataContext, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public bool Add(Course course)
        {
            _dataContext.Add(course);

            return Save();
        }

        public bool Delete(Course course)
        {
            _dataContext.Remove(course);
            return Save();
        }

        public async Task<IList<Course>> GetAllCourse() => await _dataContext.Courses.ToListAsync();

        public async Task<IList<Course>> GetAllCourseByUser()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
           
            return await _dataContext.Courses.Where(x => x.User.Id == currentUser.ToString()).ToListAsync();
        }

        public async Task<IList<Course>> GetAllCourseByUserGrupa()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userGrup = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);

            var roles = await _userManager.GetRolesAsync(userGrup);

            var coursesForRoles = await _dataContext.CourseRoles
                .Where(courseRole => roles.Contains(courseRole.Role.Name))
                .Select(courseRole => courseRole.Course)
                .Distinct()
                .ToListAsync();

            var coursesForUserGrup = await _dataContext.CourseGrupas
                .Where(cg => cg.IdGrupa == userGrup.GrupaId)
                .Select(cg => cg.Course)
                .ToListAsync();

            var combinedCourseList = coursesForRoles.Concat(coursesForUserGrup).ToList();

            return combinedCourseList;


        }


        public IList<Grupa> GetAllGrups() => _dataContext.Grupas.ToList();
        public async Task<Course> GetByIdAsync(int id) => await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Course> GetByIdAsyncNoTracking(int id) => await _dataContext.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Grupa> GetGrupaById(string id) => await _dataContext.Grupas.FirstOrDefaultAsync(x => x.Code == id);

        public async Task<User> GetUser(string id) => await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        public bool Save() => _dataContext.SaveChanges() > 0 ? true : false;

        public bool Update(Course course)
        {
            var grups = _dataContext.CourseGrupas.Where(x => x.Course.Id == course.Id).ToList();
            var roles = _dataContext.CourseRoles.Where(x => x.Course.Id == course.Id).ToList();
            if (grups.Count > 0)
                foreach (var grup in grups)
                    _dataContext.CourseGrupas.Remove(grup);
            if (roles.Count > 0)
                foreach (var role in roles)
                    _dataContext.CourseRoles.Remove(role);
            //if (anime.AnimeGenres.Count > 0)
            foreach (var grup in course.courseGrupas)
                _dataContext.CourseGrupas.Add(grup);
            foreach (var role in course.courseRoles)
                _dataContext.CourseRoles.Add(role);

            _dataContext.Update(course);
            return Save();
        }
    }
}
