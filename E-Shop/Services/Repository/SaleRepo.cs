using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories.Implementations
{
    public class SaleRepo : ISale
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public SaleRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Create(SaleViewModel saleVM)
        {
            try
            {
                var detailList = saleVM.SaleDetailsViewModels.Select(detail => new SaleDetailsModel
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    Vat = detail.Vat,
                    SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
                    StoreId = detail.StoreId
                }).ToList();

                var grandTotal = detailList.Sum(d => d.SubTotal);

                SaleModel sale = new SaleModel
                {
                    Description = saleVM.Description,
                    CustomerId = saleVM.CustomerId,
                    GrandTotal = grandTotal,
                    IsApprove = saleVM.IsApprove
                };

                db.Sales.Add(sale);
                await db.SaveChangesAsync();

                foreach (var detail in detailList)
                {
                    detail.SaleId = sale.SaleId;
                }

                db.SaleDetails.AddRange(detailList);
                await db.SaveChangesAsync();

                if (sale.IsApprove)
                {
                    foreach (var detail in detailList)
                    {
                        var ledger = await db.Ledgers.FirstOrDefaultAsync(x =>
                            x.ProductId == detail.ProductId && x.StoreId == detail.StoreId);

                        if (ledger != null)
                        {
                            ledger.Quantity -= detail.Quantity;
                            db.Ledgers.Update(ledger);
                        }
                        else
                        {
                            db.Ledgers.Add(new LedgerModel
                            {
                                ProductId = detail.ProductId,
                                Quantity = detail.Quantity,
                                Price = detail.Price,
                                InventoryTypeId = saleVM.InventoryTypeId ?? 3,
                                StockTypeId = saleVM.StockTypeId ?? 2,
                                StoreId = detail.StoreId,
                                UserId = 2
                            });
                        }
                    }
                    await db.SaveChangesAsync();
                }

                foreach (var detail in detailList)
                {
                    var stock = await db.Stocks.FirstOrDefaultAsync(a =>
                        a.ProductId == detail.ProductId && a.StoreId == detail.StoreId);

                    if (stock != null)
                    {
                        stock.StockQuantity -= detail.Quantity;
                    }
                    else
                    {
                        db.Stocks.Add(new StockModel
                        {
                            ProductId = detail.ProductId,
                            StockQuantity = detail.Quantity,
                            StoreId = detail.StoreId
                        });
                    }
                }
                await db.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        public async Task<List<SaleViewModel>> GetAll()
        {
            var list = await db.Sales.Include(s => s.SaleDetails).ToListAsync();
            return mapper.Map<List<SaleViewModel>>(list);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sale = await db.Sales.Include(p => p.SaleDetails).FirstOrDefaultAsync(p => p.SaleId == id);

            if (sale != null)
            {
                db.SaleDetails.RemoveRange(sale.SaleDetails);
                db.Sales.Remove(sale);
                await db.SaveChangesAsync();
                return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
