using System.ComponentModel.DataAnnotations;
using E_Shop.Migrations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Shop.ViewModels.Admin;

namespace E_Shop.ViewModels
{
    public class SaleViewModel
    {
        [Key]
        public int SaleId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("CustomerModel")]
        public int CustomerId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }
        public int? InventoryTypeId { get; set; }
        public int? StockTypeId { get; set; }
        public IList<SaleDetailsViewModel> SaleDetailsViewModels { get; set; } = new List<SaleDetailsViewModel>();
    }
}
