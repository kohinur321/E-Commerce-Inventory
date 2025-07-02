using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace E_Shop.ViewModels
{
    public class LedgerViewModel
    {
        public int LedgerId { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Display(Name = "Stock")]
        public int StockId { get; set; }

        [Required(ErrorMessage = "Transaction type is required")]
        [Display(Name = "Transaction Type")]
        public int TransactionTypeId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Transaction date is required")]
        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Optional: Related model names for display purposes
        public string StockName { get; set; }
        public string TransactionTypeName { get; set; }

        // Optional: For dropdowns in forms
        public IEnumerable<SelectListItem> StockList { get; set; }
        public IEnumerable<SelectListItem> TransactionTypeList { get; set; }
    }

}
