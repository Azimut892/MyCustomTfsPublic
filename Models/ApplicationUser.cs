using Microsoft.AspNetCore.Identity;



namespace MyFirstWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<TaskItem> TaskItems { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
