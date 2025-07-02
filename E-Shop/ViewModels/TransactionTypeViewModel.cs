using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class TransactionTypeViewModel
    {
        public int TransactionTypeId { get; set; }

        [Required(ErrorMessage = "Transaction type name is required")]
        [Display(Name = "Transaction Type Name")]
        public string TransactionTypeName { get; set; }

        // Optional: List of related ledgers for display, if needed
        public List<LedgerViewModel> Ledgers { get; set; } = new List<LedgerViewModel>();
    }
}
