using Microsoft.EntityFrameworkCore;
using PracticeProject.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public CourseStatus IsOpen { get; set; }


        public virtual Course Course { get; set; }
        public virtual IList<HomeWork>? HomeWork { get; set; }
        public virtual IList<TextWork>? TextWork { get; set; }
        public virtual IList<LabWork>? LabWork { get; set; }
    }
}
