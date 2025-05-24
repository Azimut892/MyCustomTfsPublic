using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
