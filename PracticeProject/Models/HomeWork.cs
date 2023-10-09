﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    public class HomeWork
    {
        [ForeignKey("Lesson")]
        public int IdLesson { get; set; }
        public int OrderNumber { get; set; }
        public string Task { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}
