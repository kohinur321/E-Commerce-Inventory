using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Shop.Models.Admin;

namespace E_Shop.Models
{
    public class PurchaseModel
    {
        [Key]
        public int PurchaseId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; } 
        public virtual SupplierModel Suppliers { get; set; }
        public IList<PurchaseDetailModel> PurchaseDetails { get; set; }
        public int? InventoryTypeId { get; internal set; }
        public int? StockTypeId { get; internal set; }
    }
}
