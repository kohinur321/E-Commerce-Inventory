using E_Shop.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.Models.Admin
{
    [Table("Order",Schema = "Admin")]
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual CustomerModel Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public bool isPending { get; set; }
        public bool isComplete { get; set; }
    }
}
