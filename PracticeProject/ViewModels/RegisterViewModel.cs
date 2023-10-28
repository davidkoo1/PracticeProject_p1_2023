using System.ComponentModel.DataAnnotations;

namespace PracticeProject.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Display(Name = "Your Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Your Surname")]
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Display(Name = "Your Grupa")]
        [Required(ErrorMessage = "Grupa is required")]
        public string GrupaId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
