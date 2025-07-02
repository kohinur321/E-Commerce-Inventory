using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class ProductTypeViewModel
    {
        [Key]
        public int ProductTypeId { get; set; }
        [Required, Display(Name = "ProductType Name")]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
