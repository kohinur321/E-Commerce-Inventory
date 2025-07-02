using E_Shop.Models.Admin;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Services.Interface
{
    public interface IProductType
    {
        Task<IActionResult> Create(ProductTypeViewModel productTypeViewModel);
        Task<List<ProductTypeViewModel>> GetAll();
        Task<IList<ProductTypeModel>> GetAllCategories();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(ProductTypeViewModel productTypeViewModel);
        Task<ProductTypeViewModel> GetById(int id);
    }
}
