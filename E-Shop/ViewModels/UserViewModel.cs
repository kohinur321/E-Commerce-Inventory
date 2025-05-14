﻿using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
