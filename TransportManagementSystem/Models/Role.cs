using System.ComponentModel.DataAnnotations;

namespace TransportManagementSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }  // Admin, Driver, Customer
    }
}
