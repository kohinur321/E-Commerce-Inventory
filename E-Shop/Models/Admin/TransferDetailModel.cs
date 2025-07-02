using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

namespace E_Shop.Models.Admin
{
    public class TransferDetailModel
    {
        [Key]
        [Display(Name = "Id")]
        public int TransferDetailId { get; set; }
        public int TransferId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int SourceStore { get; set; }
        public int DistinationStore { get; set; }

        public TransferModel Transfer { get; set; }
        public ProductModel Product { get; set; }
        public StoreModel SourceStoreNavigation { get; set; }
        public StoreModel DistinationStoreNavigation { get; set; }
    }
}
