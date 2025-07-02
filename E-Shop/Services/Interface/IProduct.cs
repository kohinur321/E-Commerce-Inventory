using System.Collections;
using E_Shop.Models.Admin;
using E_Shop.Utilities;
using E_Shop.ViewModel;

namespace E_Shop.Services.Interface
{
    public interface IProduct
    {
        Task<ResponseStatus> Create(ProductViewModel model);
        Task<ResponseStatus>Update(ProductViewModel model);
        Task<ResponseStatus> Delete(int id);
        Task<List<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(int id);
        Task<List<ProductModel>> GetAllProducts();
    }
}
