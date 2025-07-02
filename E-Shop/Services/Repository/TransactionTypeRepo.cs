using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Services.Repository
{
    public class TransactionTypeRepo : ITransaction
    {
        private readonly ApplicationDbContext _context;

        public TransactionTypeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Raw model-based method (used internally, optional)
        public async Task<IEnumerable<TransactionTypeModel>> GetAllAsyncRaw()
        {
            return await _context.Transactions
                                 .Include(t => t.Ledgers)
                                 .ToListAsync();
        }

        public async Task<TransactionTypeModel> GetByIdAsyncRaw(int id)
        {
            return await _context.Transactions
                                 .Include(t => t.Ledgers)
                                 .FirstOrDefaultAsync(t => t.TransactionTypeId == id);
        }

        // ViewModel-based method for ITransaction
        Task<IEnumerable<TransactionTypeViewModel>> ITransaction.GetAllAsync()
        {
            return Task.FromResult(_context.Transactions
                .Include(t => t.Ledgers)
                .Select(t => new TransactionTypeViewModel
                {
                    TransactionTypeId = t.TransactionTypeId,
                    TransactionTypeName = t.TransactionTypeName,
                    Ledgers = t.Ledgers.Select(l => new LedgerViewModel
                    {
                        LedgerId = l.LedgerId
                    }).ToList()
                }).AsEnumerable());
        }

        Task<TransactionTypeViewModel> ITransaction.GetByIdAsync(int id)
        {
            var transactionType = _context.Transactions
                .Include(t => t.Ledgers)
                .FirstOrDefault(t => t.TransactionTypeId == id);

            if (transactionType == null)
                return Task.FromResult<TransactionTypeViewModel>(null);

            var viewModel = new TransactionTypeViewModel
            {
                TransactionTypeId = transactionType.TransactionTypeId,
                TransactionTypeName = transactionType.TransactionTypeName,
                Ledgers = transactionType.Ledgers.Select(l => new LedgerViewModel
                {
                    LedgerId = l.LedgerId
                }).ToList()
            };

            return Task.FromResult(viewModel);
        }

        public async Task CreateAsync(TransactionTypeViewModel model)
        {
            var transactionType = new TransactionTypeModel
            {
                TransactionTypeName = model.TransactionTypeName
                // Ledgers যোগ করার দরকার থাকলে এখানে আলাদা করে যুক্ত করতে হবে
            };

            await _context.Transactions.AddAsync(transactionType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TransactionTypeViewModel model)
        {
            var transactionType = await _context.Transactions.FindAsync(model.TransactionTypeId);
            if (transactionType != null)
            {
                transactionType.TransactionTypeName = model.TransactionTypeName;
                _context.Transactions.Update(transactionType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Transactions.FindAsync(id);
            if (entity != null)
            {
                _context.Transactions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TransactionTypeModel model)
        {
            _context.Transactions.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(TransactionTypeModel model)
        {
            await _context.Transactions.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
