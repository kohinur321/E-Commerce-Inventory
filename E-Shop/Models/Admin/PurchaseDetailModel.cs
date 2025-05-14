using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_Shop.Models.Admin;

namespace E_Shop.Models
{
    public class PurchaseDetailModel
    {
        [Key]
        public int PurchaseDetailId { get; set; }
        [ForeignKey("Purchase")]
        public int PurchaseId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public virtual PurchaseModel Purchases { get; set; }
        public virtual ProductModel Products { get; set; }
        public virtual StoreModel Stores { get; set; }

    }
}
