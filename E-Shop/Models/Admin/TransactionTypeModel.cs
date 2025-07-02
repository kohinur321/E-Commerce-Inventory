using Microsoft.Extensions.Logging;

namespace E_Shop.Models.Admin
{
    using System.ComponentModel.DataAnnotations;  

    public class TransactionTypeModel
    {
        [Key]  
        public int TransactionTypeId { get; set; }

        [Required]
        public string TransactionTypeName { get; set; }

        public ICollection<LedgerModel> Ledgers { get; set; }  
    }

}
