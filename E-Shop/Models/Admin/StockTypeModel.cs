using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models
{
    public class StockTypeModel
    {
        [Key]
        public int StockTypeId { get; set; }
        [StringLength(100)]
        public string StockTypeName { get; set; }
    }
}
