using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Shop.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage ="Product Name is Required")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }
        [Display(Name = "Unit")]
        public string Unit { get; set; }
        public string ImageUrl { get; set; } = "Noimage.png";
        [NotMapped]
        [Display(Name = "Company Logo")]
        public IFormFile Image { get; set; }
        [NotMapped]
        public string ImageName { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        public bool isAvailable { get; set; }
    }
}
