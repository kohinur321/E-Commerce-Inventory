using System.ComponentModel.DataAnnotations;
using E_Shop.Migrations;

namespace E_Shop.Models.Admin
{
    public class SaleDetailsModel
    {
        [Key]
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        public int StoreId { get; set; }

        public virtual SaleModel Sale { get; set; }  // Relationship with Sale
        public virtual ProductModel Product { get; set; }  // Relationship with Product
        public virtual StoreModel Store { get; set; }  // Relationship with S
    }
}
