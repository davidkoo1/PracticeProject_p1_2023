using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeProject.Models
{
    [PrimaryKey("IdCourse", "RoleId")]
    public class CourseRole
    {
        [ForeignKey("Course"), Column(Order = 0)]
        public int IdCourse { get; set; }

        [ForeignKey("IdentityRole"), Column(Order = 1)]
        public string RoleId { get; set; }

        public virtual Course Course { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}
