namespace PracticeProject.ViewModels
{
    //all grups - ViewBag, allRoles - ViewBag
    public class EditProfileViewModel
    {
        public string Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Grupa { get; set; }
        public List<string> Roles { get; set; }
    }
}
