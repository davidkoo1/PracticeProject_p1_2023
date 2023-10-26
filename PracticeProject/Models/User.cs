using Microsoft.AspNetCore.Identity;

namespace PracticeProject.Models
{
    //для учителей вкладку по типу мои курсы
    public class User : IdentityUser
    {
        public string? GrupaId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ProfileImage { get; set; }
        public virtual Grupa? Grupa { get; set; }
        public virtual IList<Course>? Course { get; set; }

        //список дз к примеру

    }
}
