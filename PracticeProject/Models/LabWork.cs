using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    public class LabWork
    {
        [Key]
        public int Id { get; set; }
        public string Task { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
