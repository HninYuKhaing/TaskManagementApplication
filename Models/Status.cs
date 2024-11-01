﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagementApplication.Models
{
    public class Status
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Status name is required.")]
        [StringLength(50)]
        public string Name { get; set; }
        public string Color { get; set; } = "#FFFFFF";

        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}