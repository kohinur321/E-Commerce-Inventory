namespace E_Shop.Services.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using E_Shop.Models.Admin;
    using E_Shop.ViewModels;

    public interface ITransaction
    {
        Task<IEnumerable<TransactionTypeViewModel>> GetAllAsync();
        Task<TransactionTypeViewModel> GetByIdAsync(int id);
        Task CreateAsync(TransactionTypeViewModel model);
        Task UpdateAsync(TransactionTypeViewModel model);
        Task DeleteAsync(int id);
        Task UpdateAsync(TransactionTypeModel model);
        Task AddAsync(TransactionTypeModel model);
    }

}
