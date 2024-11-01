using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TaskManagementApplication.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}