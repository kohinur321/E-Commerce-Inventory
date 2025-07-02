using E_Shop.Models.Admin;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Services.Interface
{
    public interface ISale
    {
        Task<IActionResult> Create(SaleViewModel saleVM);
        Task<List<SaleViewModel>> GetAll();
        Task<IActionResult> Delete(int id);
    }
}
