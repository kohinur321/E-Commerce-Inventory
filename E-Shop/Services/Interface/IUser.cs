using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Services.Interface
{
    public interface IUser
    {
        Task<IActionResult> Create(UserViewModel userViewModel);
        Task<List<UserViewModel>> GetAll();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(UserViewModel userViewModel);
        Task<UserViewModel> GetById(int id);
    }
}
