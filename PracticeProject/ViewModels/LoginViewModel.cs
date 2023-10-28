using System.ComponentModel.DataAnnotations;

namespace PracticeProject.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address or username")]
        [Required(ErrorMessage = "Email or username is required")]
        public string ConfirmData { get; set; } //UserName, Email
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
}
