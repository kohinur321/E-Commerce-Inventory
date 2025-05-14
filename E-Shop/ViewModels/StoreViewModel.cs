using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class StoreViewModel
    {
        [Key]
        public int StoreId { get; set; }

        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store Name is Required")]
        [StringLength(100)]
        public string StoreName { get; set; }
    }
}
