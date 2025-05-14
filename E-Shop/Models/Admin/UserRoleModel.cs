using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.Models.Admin
{
    public class UserRoleModel
    {
        [Key]
        public int UserRoleId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual UserModel UserModel { get; set; }
        public virtual RoleModel RoleModel { get; set; }
    }
}
