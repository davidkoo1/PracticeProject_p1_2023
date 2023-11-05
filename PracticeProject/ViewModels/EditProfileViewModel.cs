namespace PracticeProject.ViewModels
{
    public class EditProfileViewModel
    {
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public List<string> Roles { get; set; }
    }
}
