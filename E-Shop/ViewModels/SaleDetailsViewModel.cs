using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.ViewModels.Admin
{
    public class SaleDetailsViewModel
    {
        public int SaleDetailsId { get; set; }
        [ForeignKey("SaleModel")]
        public int SaleId { get; set; }
        [ForeignKey("ProductModel")]
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Vat { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey("StoreModel")]
        public int StoreId { get; set; }  
        [ForeignKey("CustomerModel")]
        public int CustomerId { get; set; }
        //public TimeSpan? StartTime { get; set; }
        //public TimeSpan? EndTime { get; set; }
    }
}
