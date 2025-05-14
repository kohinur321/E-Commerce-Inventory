using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Shop.ViewModels
{
    public class UserRoleViewModel
    {
        public int UserRoleId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } // ইউজারের নাম দেখানোর জন্য

        public int RoleId { get; set; }
        public string RoleName { get; set; } // রোলের নাম দেখানোর জন্য
        public List<SelectListItem> Roles { get; internal set; }
        public List<SelectListItem> UserModel { get; internal set; }
        public List<SelectListItem> Users { get; internal set; }
    }
}
