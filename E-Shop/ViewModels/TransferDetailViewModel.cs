namespace E_Shop.ViewModels
{
    public class TransferDetailViewModel
    {
        public int TransferDetailId { get; set; }
        public int TransferId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int SourceStore { get; set; }
        public int DistinationStore { get; set; }

        // Optional: These are for display purposes (Dropdowns or Labels)
        public string ProductName { get; set; }
        public string SourceStoreName { get; set; }
        public string DistinationStoreName { get; set; }
    }

}
