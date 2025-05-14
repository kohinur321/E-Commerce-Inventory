using E_Shop.Models.User;
using E_Shop.Utilities;
using E_Shop.ViewModel;

namespace E_Shop.Services.Interface
{
    public interface ICustomer
    {
        Task<ResponseStatus>Save(CustomerViewModel model);
    }
}
