using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Admin
{
    public class RoleModel
    {
        [Key]
        public int RoleId { get; set; }
        [StringLength(100)]
        public string RoleName { get; set; }
    }
}
