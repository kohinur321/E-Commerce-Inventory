using E_Shop.Utilities;
using E_Shop.ViewModel;

namespace E_Shop.Services.Interface
{
    public interface IOrder
    {
        Task<List<OrderViewModel>> GetAll();
        Task<OrderViewModel> GetById(int id);
        Task<List<CartViewModel>> GetItemDetails(int id);
        Task<ResponseStatus>ConfirmOrder(OrderViewModel model);
        Task<List<OrderViewModel>> GetCompleteOrder();
    }
}
