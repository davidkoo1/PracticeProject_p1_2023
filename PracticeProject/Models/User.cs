using Microsoft.AspNetCore.Identity;

namespace PracticeProject.Models
{
    //для учителей вкладку по типу мои курсы
    public class User : IdentityUser
    {
        public string? Grupa { get; set; }
        public string? ProfileImage { get; set; }
        public virtual IList<Course>? Course { get; set; }

        //список дз к примеру

    }
}
