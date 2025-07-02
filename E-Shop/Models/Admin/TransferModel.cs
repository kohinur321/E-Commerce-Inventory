using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Admin
{
    public class TransferModel
    {
        [Key]
        public int TransferId { get; set; }
        public string Description { get; set; }
        public bool IsApprove { get; set; }
        
        public ICollection<TransferDetailModel> TransferDetails { get; set; }
    }
}
