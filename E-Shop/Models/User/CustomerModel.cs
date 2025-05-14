using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.Models.User
{
    [Table("Customer",Schema ="User")]
    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
    }
}
