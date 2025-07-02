using E_Shop.ViewModels;

namespace E_Shop.Services.Interface
{
    public interface IDamageDetails
    {
        Task<IEnumerable<DamageDetailViewModel>> GetAllAsync();
        Task<DamageDetailViewModel> GetByIdAsync(int id);
        Task AddAsync(DamageDetailViewModel model);
        Task UpdateAsync(DamageDetailViewModel model);
        Task DeleteAsync(int id);
    }
}
