using E_Shop.Models.Admin;
using E_Shop.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace E_Shop.ViewModel
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Total Price")]
        public double TotalPrice{ get; set; }
        public int OrderId { get; set; }
    }
}
