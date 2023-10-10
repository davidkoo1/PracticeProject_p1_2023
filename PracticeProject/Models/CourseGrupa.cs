using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    public class CourseGrupa
    {
        //[ForeignKey("Course")]
        public int IdCourse { get; set; }

        //[ForeignKey("Grupa")]
        public string IdGrupa { get; set; }

        public virtual Grupa Grupa { get; set; }
        public virtual Course Course { get; set; }
    }
}
