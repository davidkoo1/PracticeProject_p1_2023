using Microsoft.EntityFrameworkCore;

namespace PracticeProject.Models
{
    [PrimaryKey("Code")]
    public class Grupa
    {
        public string Code { get; set; }
        public string FullName { get; set; }

        public virtual IEnumerable<CourseGrupa> courseGrupas { get; set; }
    }
}
