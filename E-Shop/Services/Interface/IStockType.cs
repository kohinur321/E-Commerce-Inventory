using Microsoft.AspNetCore.Mvc;
using E_Shop.Models;
using E_Shop.ViewModels;
using System.Collections;

namespace E_Shop.Service.Interface
{
    public interface IStockType
    {
        Task<IActionResult> Create(StockTypeViewModel stockTypeViewModel);
        Task<List<StockTypeViewModel>> GetAll();
        Task<IList<StockTypeModel>> GetAllStockType();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(StockTypeViewModel stockTypeViewModel);
        Task<StockTypeViewModel> GetById(int id);
        Task<IEnumerable> GetAllAsync();
    }
}
