using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Shop.ViewModels
{
    public class StockViewModel
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }  // Display purpose

        public int StoreId { get; set; }
        public string StoreName { get; set; }    // Display purpose

        public int StockQuantity { get; set; }

        public int StockTypeId { get; set; }
        public string StockTypeName { get; set; }  // Optional: Friendly name for stock type

        public DateTime LastUpdated { get; set; }

        // Optional: dropdowns for UI
       // public IEnumerable<SelectListItem> ProductList { get; set; }
       // public IEnumerable<SelectListItem> StoreList { get; set; }
        //public IEnumerable<SelectListItem> StockTypeList { get; set; }
    }

}
