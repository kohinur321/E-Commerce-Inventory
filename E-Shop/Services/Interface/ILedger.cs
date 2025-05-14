using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Shop.Models.Admin;
using E_Shop.ViewModels;

public interface ILedger
{
    Task<IEnumerable<LedgerViewModel>> GetAllLedgersAsync();
    Task<LedgerViewModel> GetLedgerByIdAsync(int ledgerId);
    Task AddLedgerAsync(LedgerModel ledger);
    Task UpdateLedgerAsync(LedgerModel ledger);
    Task DeleteLedgerAsync(int ledgerId);
    //Task<string> GetAllAsync();
}
