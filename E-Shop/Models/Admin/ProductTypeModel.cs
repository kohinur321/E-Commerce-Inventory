using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Admin
{
    public class ProductTypeModel
    {
        [Key]
        public int ProductTypeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
