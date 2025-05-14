using E_Shop.ViewModels;

namespace E_Shop.Services.Interface
{
    public interface IUserRole
    {
        Task<List<UserRoleViewModel>> GetAll();
        Task<UserRoleViewModel> GetById(int id);
        Task<bool> Create(UserRoleViewModel model);
        Task<bool> Update(UserRoleViewModel model);
        Task<bool> Delete(int id);
    }
}
