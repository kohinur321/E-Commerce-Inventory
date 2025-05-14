using E_Shop.Data;
using E_Shop.Models;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_Shop.Services.Repository
{
    public class PurchaseRepo: IPurchase
    {
        private readonly ApplicationDbContext _context;

        public PurchaseRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(PurchaseViewModel model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var purchase = new PurchaseModel
                {
                    Description = model.Description,
                    SupplierId = model.SupplierId,
                    GrandTotal = model.GrandTotal,
                    IsApprove = model.IsApprove,
                    InventoryTypeId = model.InventoryTypeId,
                    StockTypeId = model.StockTypeId
                };

                _context.Purchases.Add(purchase);
                await _context.SaveChangesAsync();

                foreach (var detail in model.PurchaseDetailViewModel)
                {
                    var purchaseDetail = new PurchaseDetailModel
                    {
                        PurchaseId = purchase.PurchaseId,
                        ProductId = detail.ProductId,
                        Price = detail.Price,
                        Quantity = detail.Quantity,
                        Vat = detail.Vat,
                        SubTotal = detail.SubTotal,
                        StoreId = detail.StoreId
                    };
                    _context.PurchaseDetails.Add(purchaseDetail);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<PurchaseViewModel> GetByIdAsync(int id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .FirstOrDefaultAsync(p => p.PurchaseId == id);

            if (purchase == null) return null;

            return new PurchaseViewModel
            {
                PurchaseId = purchase.PurchaseId,
                Description = purchase.Description,
                SupplierId = purchase.SupplierId,
                GrandTotal = purchase.GrandTotal,
                IsApprove = purchase.IsApprove,
                InventoryTypeId = purchase.InventoryTypeId,
                StockTypeId = purchase.StockTypeId,
                PurchaseDetailViewModel = purchase.PurchaseDetails.Select(d => new PurchaseDetailViewModel
                {
                    PurchaseDetailId = d.PurchaseDetailId,
                    ProductId = d.ProductId,
                    Price = d.Price,
                    Quantity = d.Quantity,
                    Vat = d.Vat,
                    SubTotal = d.SubTotal,
                    StoreId = d.StoreId
                }).ToList()
            };
        }

        public async Task<List<PurchaseViewModel>> GetAllAsync()
        {
            return await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .Select(p => new PurchaseViewModel
                {
                    PurchaseId = p.PurchaseId,
                    Description = p.Description,
                    SupplierId = p.SupplierId,
                    GrandTotal = p.GrandTotal,
                    IsApprove = p.IsApprove,
                    InventoryTypeId = p.InventoryTypeId,
                    StockTypeId = p.StockTypeId,
                    PurchaseDetailViewModel = p.PurchaseDetails.Select(d => new PurchaseDetailViewModel
                    {
                        PurchaseDetailId = d.PurchaseDetailId,
                        ProductId = d.ProductId,
                        Price = d.Price,
                        Quantity = d.Quantity,
                        Vat = d.Vat,
                        SubTotal = d.SubTotal,
                        StoreId = d.StoreId
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(PurchaseViewModel model)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .FirstOrDefaultAsync(p => p.PurchaseId == model.PurchaseId);

            if (purchase == null) return false;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Update master
                purchase.Description = model.Description;
                purchase.SupplierId = model.SupplierId;
                purchase.GrandTotal = model.GrandTotal;
                purchase.IsApprove = model.IsApprove;
                purchase.InventoryTypeId = model.InventoryTypeId;
                purchase.StockTypeId = model.StockTypeId;

                // Delete existing details
                _context.PurchaseDetails.RemoveRange(purchase.PurchaseDetails);
                await _context.SaveChangesAsync();

                // Add new details
                foreach (var detail in model.PurchaseDetailViewModel)
                {
                    var newDetail = new PurchaseDetailModel
                    {
                        PurchaseId = purchase.PurchaseId,
                        ProductId = detail.ProductId,
                        Price = detail.Price,
                        Quantity = detail.Quantity,
                        Vat = detail.Vat,
                        SubTotal = detail.SubTotal,
                        StoreId = detail.StoreId
                    };
                    _context.PurchaseDetails.Add(newDetail);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var purchase = await _context.Purchases
                .Include(p => p.PurchaseDetails)
                .FirstOrDefaultAsync(p => p.PurchaseId == id);

            if (purchase == null) return false;

            _context.PurchaseDetails.RemoveRange(purchase.PurchaseDetails);
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}