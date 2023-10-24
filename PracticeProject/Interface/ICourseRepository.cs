using PracticeProject.Models;

namespace PracticeProject.Interface
{
    public interface ICourseRepository
    {
        Task<IList<Course>> GetAllCourse();
        Task<Grupa> GetGrupaById(string id);
        IList<Grupa> GetAllGrups();
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdAsyncNoTracking(int id);
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
