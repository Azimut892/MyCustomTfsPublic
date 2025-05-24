namespace MyFirstWebApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerId { get; set; }
        public ApplicationUser Manager { get; set; }
        public ICollection<ApplicationUser> Members { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
