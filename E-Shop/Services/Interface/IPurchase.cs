using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Services.Interface
{
    public interface IPurchase
    {
        //Task<IActionResult> ApprovePurchase(int purchaseId);
        Task<List<PurchaseViewModel>> GetAll();
        Task<PurchaseViewModel> GetById(int id);
        Task<PurchaseViewModel> CreateMaster(PurchaseViewModel purchaseViewModel);
        Task<PurchaseDetailViewModel> CreateDetail(PurchaseDetailViewModel detail);
        Task<PurchaseViewModel> Approve(PurchaseViewModel purchaseViewModel);
        Task<PurchaseDetailViewModel> RemoveDetail(int id);
    }
}
