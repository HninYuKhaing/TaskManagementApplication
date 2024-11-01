using System.ComponentModel.DataAnnotations;

namespace TaskManagementApplication.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}