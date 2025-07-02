using System.ComponentModel.DataAnnotations;
using E_Shop.Migrations;
using E_Shop.Service.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.Models.Admin
{

    public class LedgerModel
    {
        [Key]
        public int LedgerId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("InventoryType")]
        public int InventoryTypeId { get; set; }
        [ForeignKey("Users")]
        public int? UserId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("StockType")]
        public int StockTypeId { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual InventoryTypeModel InventoryType { get; set; }
        public virtual UserModel User { get; set; }
        public virtual StockTypeModel StockType { get; set; }
        public virtual StoreModel Store { get; set; }
    }

}
