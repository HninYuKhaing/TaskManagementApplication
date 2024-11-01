using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApplication.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Status is required.")]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Priority is required.")]
        public int PriorityId { get; set; }

        [ForeignKey("PriorityId")]
        public virtual Priority Priority { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [Required(ErrorMessage = "Assignee is required.")]
        public int AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public virtual User Assignee { get; set; }
    }
}