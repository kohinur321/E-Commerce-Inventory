using System.Collections;
using E_Shop.Models.Admin;
using E_Shop.Utilities;
using E_Shop.ViewModels;

namespace E_Shop.Services.Interface
{
    public interface ISupplier
    {
        Task<ResponseStatus> Create(SupplierViewModel model);
        Task<ResponseStatus> Update(SupplierViewModel model);
        Task<ResponseStatus> Delete(int id);
        Task<List<SupplierViewModel>> GetAll();
        Task<SupplierViewModel> GetById(int id);
        Task<IEnumerable> GetAllAsync();
        Task<List<SupplierModel>> GetAllSupplier();
    }
}
