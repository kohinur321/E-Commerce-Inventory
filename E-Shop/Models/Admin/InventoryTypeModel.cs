using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models
{
    public class InventoryTypeModel
    {
        [Key]
        public int InventoryTypeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public string Remarks { get; set; }
    }
}
