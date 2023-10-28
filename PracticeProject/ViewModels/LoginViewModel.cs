using System.ComponentModel.DataAnnotations;

namespace PracticeProject.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string EmailAddress { get; set; } //UserName, Email
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
}
