using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
