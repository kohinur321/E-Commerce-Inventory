using E_Shop.ViewModels;

namespace E_Shop.Services.Interface
{
    public interface IDamage
    {
        Task<IEnumerable<DamageViewModel>> GetAllDamagesAsync();
        Task<DamageViewModel> GetDamageByIdAsync(int id);
        Task<int> CreateDamageAsync(DamageViewModel damage);
        Task<bool> UpdateDamageAsync(DamageViewModel damage);
       Task<bool> DeleteDamageAsync(int id);
        Task<bool> ApproveDamageAsync(int id);
    }
}
