using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Admin
{
    public class DamageModel
    {
        [Key]
        public int DamageId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public decimal GrandTotal { get; set; }
        public bool IsApprove { get; set; }

        public IList<DamageDetailModel> DamageDetails { get; set; }
    }
}
