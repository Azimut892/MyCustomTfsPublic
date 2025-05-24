using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


//view -> other windows -> Package Manager Console -> Default project dropdown is set to your main project
//FIRST CREATE: in console run "Add-Migration InitialCreate" and then run "Update-Database" for first creation
//UPDATE: in console run "Add-Migration 'changeDescription'" and then run "Update-Database" for updates


namespace MyFirstWebApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(1);
        [Display(Name = "Estimated Time (h)")]
        public int EstimatedTime { get; set; }
        public TaskState Status { get; set; }
        [ForeignKey(nameof(AssignedToUser))]
        [Display(Name = "Assigne To")]
        public string? AssignedToUserId { get; set; }
        public ApplicationUser AssignedToUser { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public int? ProjectId { get; set; }
        public ProjectModel Project { get; set; }
    }
    public enum TaskState
    {
        [Display(Name = "New")]
        New,
        [Display(Name = "Defined")]
        Defined,
        [Display(Name = "In Work")]
        InWork,
        [Display(Name = "Completed")]
        Completed

    }
}
