using System.Collections;
using E_Shop.Models.Admin;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Services.Interface
{
    public interface IStore
    {
        Task<IActionResult> Create(StoreViewModel storeViewModel);
        Task<List<StoreViewModel>> GetAll();
        Task<StoreViewModel> GetById(int id);
        Task<IActionResult> Update(StoreViewModel storeViewModel);
        Task<IActionResult> Delete(int id);
        Task<IEnumerable> GetAllAsync();
        Task<List<StoreModel>> GetAllStores();
    }
}
