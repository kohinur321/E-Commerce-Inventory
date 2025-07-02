using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PurchaseDetailViewModel
    {
        public int PurchaseDetailId { get; set; }

        public int PurchaseId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Vat { get; set; }

        public decimal SubTotal { get; set; }

        public int StoreId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string StoreName { get; set; } = string.Empty;


    }

}
