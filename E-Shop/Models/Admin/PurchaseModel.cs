using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;

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
        public virtual SupplierModel Supplier { get; set; }
        public IList<PurchaseDetailModel> PurchaseDetailsModel { get; set; }

    }
}
