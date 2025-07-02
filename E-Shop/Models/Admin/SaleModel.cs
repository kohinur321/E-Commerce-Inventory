using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Shop.Models.User;

namespace E_Shop.Models.Admin
{
    public class SaleModel
    {
        [Key]
        public int SaleId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("CustomerModel")]
        public int CustomerId { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }
        public bool IsPaid { get; set; }

        public virtual CustomerModel Customer { get; set; }
        public IList<SaleDetailsModel> SaleDetails { get; set; }
    }
}
