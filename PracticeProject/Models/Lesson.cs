using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    [PrimaryKey("Id")]
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTextWork { get; set; }
        public bool IsLabWork { get; set; }
        public bool IsHomeWork { get; set; }

        [ForeignKey("Course")]
        public int IdCourse { get; set; }

        public virtual Course Course { get; set; }

    }
}
