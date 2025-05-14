using E_Shop.Models.Admin;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.Models.User
{
    [Table("Cart", Schema ="User")]
    public class CartModel
    {
        [Key]
        public int CartId { get; set; }
        [ForeignKey("Product")]
        public int ProductId{ get; set; }
        public virtual ProductModel Product { get; set; }
        public int Quantity{ get; set; }
        public double TotalPrice { get; set; }
        public int OrderId{ get; set; }
    }
}
