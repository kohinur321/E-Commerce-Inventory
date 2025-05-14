using E_Shop.Utilities;
using E_Shop.ViewModel;

namespace E_Shop.Services.Interface
{
    public interface ICart
    {
        Task<ResponseStatus>Create(CartViewModel model);
        Task<List<CartViewModel>> GetAll();
        Task<ResponseStatus> Delete(int id);
    }
}
