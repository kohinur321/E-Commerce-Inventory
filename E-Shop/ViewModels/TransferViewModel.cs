using System.ComponentModel.DataAnnotations;

namespace E_Shop.ViewModels
{
    public class TransferViewModel
    {
        public int TransferId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public bool IsApprove { get; set; }

        public List<TransferDetailViewModel> TransferDetails { get; set; } = new();
    }

}
