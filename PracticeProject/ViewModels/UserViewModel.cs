namespace PracticeProject.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Grupa { get; set; }
        public string? Image { get; set; }
        public List<string> RolesName { get; set; }
    }
}
