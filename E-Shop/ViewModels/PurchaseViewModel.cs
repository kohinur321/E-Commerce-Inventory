using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace E_Shop.ViewModels
{
    public class PurchaseViewModel
    {
        [Key]

        public int PurchaseId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }
        //public string SupplierName { get; set; }
        public int? InventoryTypeId { get; set; }
        public int? StockTypeId { get; set; }

        public IList<PurchaseDetailViewModel> PurchaseDetailViewModel { get; set; } = new List<PurchaseDetailViewModel>();
    }
}
