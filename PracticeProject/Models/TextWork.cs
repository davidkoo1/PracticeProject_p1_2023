using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    public class TextWork
    {
        [ForeignKey("Lesson")]
        public int IdLesson { get; set; }
        public int OrderNumber { get; set; }
        public string Text { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
