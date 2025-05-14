using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LedgerRepo: ILedger
{
    private readonly ApplicationDbContext _context;

    public LedgerRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LedgerModel>> GetAllAsync()
    {
        return await _context.Ledgers
            .Include(l => l.StockModel)
            //.Include(l => l.TransactionType) // Uncomment if using
            .ToListAsync();
    }

    public async Task<LedgerModel> GetByIdAsync(int ledgerId)
    {
        return await _context.Ledgers
            .Include(l => l.StockModel)
            //.Include(l => l.TransactionType)
            .FirstOrDefaultAsync(l => l.LedgerId == ledgerId);
    }

    public async Task AddAsync(LedgerModel ledger)
    {
        _context.Ledgers.Add(ledger);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LedgerModel ledger)
    {
        _context.Ledgers.Update(ledger);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int ledgerId)
    {
        var ledger = await _context.Ledgers.FindAsync(ledgerId);
        if (ledger != null)
        {
            _context.Ledgers.Remove(ledger);
            await _context.SaveChangesAsync();
        }
    }

    public Task<IEnumerable<LedgerViewModel>> GetAllLedgersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LedgerViewModel> GetLedgerByIdAsync(int ledgerId)
    {
        throw new NotImplementedException();
    }

    public Task AddLedgerAsync(LedgerModel ledger)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLedgerAsync(LedgerModel ledger)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLedgerAsync(int ledgerId)
    {
        throw new NotImplementedException();
    }
}
