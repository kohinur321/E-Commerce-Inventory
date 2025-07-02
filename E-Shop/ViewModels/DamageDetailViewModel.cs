using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class DamageDetailViewModel
    {
        public int DamageDetailId { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Display(Name = "VAT")]
        [DataType(DataType.Currency)]
        public decimal Vat { get; set; }

        [Display(Name = "Sub Total")]
        [DataType(DataType.Currency)]
        public decimal SubTotal { get; set; }

        [Required]
        [Display(Name = "Store")]
        public int StoreId { get; set; }

        public int DamageId { get; set; }

        // Optional: display-only fields
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
    }
}
