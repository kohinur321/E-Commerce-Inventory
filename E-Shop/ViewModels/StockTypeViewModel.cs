using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class StockTypeViewModel
    {
        [Key]
        public int StockTypeId { get; set; }
        [Required,Display(Name ="StockType Name")]
        [StringLength(100)]
        public string StockTypeName { get; set; }
    }
}
