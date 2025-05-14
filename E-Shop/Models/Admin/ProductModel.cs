using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.Models.Admin
{
    [Table("Product",Schema = "Admin")]
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public double Price { get; set; }
        [StringLength(100)]
        public string Unit { get; set; }
        [StringLength(250)]
        public string ImageUrl { get; set; } = "noimage.png";
        [NotMapped]
        public IFormFile Image { get; set; }
        [NotMapped]
        public string ImageName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool isAvailable { get; set; }
    }
}
