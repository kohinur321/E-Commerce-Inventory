using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Admin
{
    public class StoreModel
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreName { get; set; }
    }
}
