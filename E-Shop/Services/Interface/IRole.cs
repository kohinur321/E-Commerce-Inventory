using E_Shop.Models.Admin;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Services.Interface
{
    public interface IRole
    {
        Task<IActionResult> Create(RoleViewModel roleViewModel);
        Task<List<RoleViewModel>> GetAll();
        Task<IActionResult> Delete(int id);
        Task<IActionResult> Update(RoleViewModel roleViewModel);
        Task<RoleViewModel> GetById(int id);
    }
}
