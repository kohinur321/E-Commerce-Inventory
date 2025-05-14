using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Shop.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        public int OrderId { get; set; }
   }
}
