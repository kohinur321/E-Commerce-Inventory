using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Admin
{
    public class DamageDetailModel
    {
        [Key]
        public int DamageDetailId { get; set; }
        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey("StoreModel")]
        public int StoreId { get; set; }
        [ForeignKey("DamageModel")]
        public int DamageId { get; set; }
        public virtual DamageModel Damage { get; set; }
        public virtual ProductModel Product { get; set; }
        public virtual StoreModel Store { get; set; }
    }
}
