﻿using Microsoft.EntityFrameworkCore;
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

        public CourseRepository(ApplicationDbContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
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
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            var userGrup = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == currentUser);


            return await _dataContext.CourseGrupas
                .Where(cg => cg.IdGrupa == userGrup.GrupaId)
                .Select(cg => cg.Course)
                .ToListAsync();

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
            if (grups.Count > 0)
                foreach (var grup in grups)
                    _dataContext.CourseGrupas.Remove(grup);
            //if (anime.AnimeGenres.Count > 0)
            foreach (var grup in course.courseGrupas)
                _dataContext.CourseGrupas.Add(grup);
            _dataContext.Update(course);
            return Save();
        }
    }
}
