using System.Collections;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace E_Shop.Service.Interface
{
    public interface IInventoryType
    {
        Task<IActionResult> Create(InventoryTypeViewModel inventoryTypeViewModel);
        Task<List<InventoryTypeViewModel>> GetAll();
        Task<IList<InventoryTypeViewModel>> GetAllInventoryType();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(InventoryTypeViewModel inventoryTypeViewModel);
        Task<InventoryTypeViewModel> GetById(int id);
        Task<IEnumerable> GetAllAsync();
    }
}
