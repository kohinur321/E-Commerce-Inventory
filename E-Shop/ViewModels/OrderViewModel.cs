using E_Shop.Models.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Shop.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        [Display(Name = "Customer Details")]
        public int CustomerId { get; set; }
        public virtual CustomerModel Customer { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool isPending { get; set; }
        public bool isComplete { get; set; }
    }
}

