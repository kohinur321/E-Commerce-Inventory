namespace E_Shop.ViewModels
{
    public class LedgerViewModel
    {
        public int LedgerId { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; }          // Derived from StockModel
        public string TransactionType { get; set; }    // Could be resolved from TransactionTypeId
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }

        // Optional: formatted date string for UI
        public string TransactionDateFormatted => TransactionDate.ToString("yyyy-MM-dd");

        // Optional: human-friendly formatted amount
        public string AmountFormatted => Amount.ToString("C");
    }

}
