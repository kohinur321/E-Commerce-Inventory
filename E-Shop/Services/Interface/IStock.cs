using E_Shop.Models.Admin;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Shop.Models.Admin
{
    public interface IStock
    {
        Task<IEnumerable<StockModel>> GetAllAsync();
        Task<StockModel> GetByIdAsync(int stockId);
        Task AddAsync(StockModel stock);
        Task UpdateAsync(StockModel stock);
        Task DeleteAsync(int stockId);
    }
}
