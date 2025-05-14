using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Shop.Models.Admin
{
    public class StockModel
    {
        [Key]
        public int StockId { get; set; }

        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int StockQuantity { get; set; }
        public int StockTypeId { get; set; }
        public DateTime LastUpdated { get; set; }

        public ProductModel ProductModel { get; set; }
        public StoreModel StoreModel { get; set; }

    }
}
