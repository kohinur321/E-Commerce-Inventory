using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Admin
{
    public class LedgerModel
    {
        [Key]
        public int LedgerId { get; set; }
        public int StockId { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }

        public StockModel StockModel { get; set; }  // Relationship with Stock
       // public TransactionType TransactionType { get; set; }  // Relationship with TransactionType
    }
}
