using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class RoleViewModel
    {
        [Key]
        public int RoleId { get; set; }
        [StringLength(100)]
        public string RoleName { get; set; }
    }
}
