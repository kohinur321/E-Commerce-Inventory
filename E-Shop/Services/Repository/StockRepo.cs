using E_Shop.Data;
using E_Shop.Models.Admin;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StockRepo : IStock
{
    private readonly ApplicationDbContext _context;

    public StockRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StockModel>> GetAllAsync()
    {
        return await _context.Stocks
            .Include(s => s.ProductModel)
            .Include(s => s.StoreModel)
            .ToListAsync();
    }

    public async Task<StockModel> GetByIdAsync(int stockId)
    {
        return await _context.Stocks
            .Include(s => s.ProductModel)
            .Include(s => s.StoreModel)
            .FirstOrDefaultAsync(s => s.StockId == stockId);
    }

    public async Task AddAsync(StockModel stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StockModel stock)
    {
        _context.Stocks.Update(stock);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int stockId)
    {
        var stock = await _context.Stocks.FindAsync(stockId);
        if (stock != null)
        {
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }
    }
}
