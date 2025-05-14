using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Services.Interface
{
    public interface IPurchase
    {
        Task<bool> CreateAsync(PurchaseViewModel purchaseViewModel);
        Task<PurchaseViewModel> GetByIdAsync(int id);
        Task<List<PurchaseViewModel>> GetAllAsync();
        Task<bool> UpdateAsync(PurchaseViewModel purchaseViewModel);
        Task<bool> DeleteAsync(int id);
    }
}
