using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    public class HomeWork
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Task { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
