using PracticeProject.Models;

namespace PracticeProject.Interfaces
{
    public interface ICourseRepository
    {
        Task<IList<Course>> GetAllCourse();
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdAsyncNoTraking(int id);
        /*
        Task<IList<AnimeGenre>> GetAnimesByGenre(string genre);
        Task<IList<Anime>> GetAnimeByGenres(string[] genres);
        Task<IList<Anime>> GetAnimeByEditor(string Editor);
        */
        bool Add(Course course);
        bool Update(Course course);
        bool Delete(Course course);
        bool Save();
    }
}
