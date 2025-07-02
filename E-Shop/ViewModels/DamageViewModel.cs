using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class DamageViewModel
    {
        public int DamageId { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Grand Total")]
        [DataType(DataType.Currency)]
        public decimal GrandTotal { get; set; }

        [Display(Name = "Approved")]
        public bool IsApprove { get; set; }

        public List<DamageDetailViewModel> DamageDetails { get; set; } = new();
    }
}
