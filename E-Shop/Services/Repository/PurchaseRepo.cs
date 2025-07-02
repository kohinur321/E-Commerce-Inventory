using AutoMapper;
using E_Shop.Data;
using E_Shop.Models;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace E_Shop.Services.Repository
{
    public class PurchaseRepo : IPurchase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public PurchaseRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<PurchaseViewModel>> GetAll()
        {
            //var list = await db.Purchases.ToListAsync();
            var list = await (from p in db.Purchases
                              join s in db.Suppliers on p.SupplierId equals s.SupplierId
                              select new PurchaseViewModel()
                              {
                                  SupplierId = p.SupplierId,
                                  Description = p.Description,
                                  GrandTotal = p.GrandTotal,
                                  SupplierName = s.SupplierName,
                                  PurchaseId = p.PurchaseId,
                                  IsApprove = p.IsApprove,
                              }).ToListAsync();
            //var purchaselist = mapper.Map<List<PurchaseVM>>(list);
            return list;
        }

        public async Task<PurchaseViewModel> GetById(int id)
        {
            PurchaseViewModel oPurchase = null; decimal grandTotal = 0;
            var purchase = await (from p in db.Purchases
                                  join s in db.Suppliers on p.SupplierId equals s.SupplierId
                                  where p.PurchaseId == id
                                  select new PurchaseViewModel()
                                  {
                                      SupplierId = p.SupplierId,
                                      Description = p.Description,
                                      GrandTotal = p.GrandTotal,
                                      SupplierName = s.SupplierName,
                                      PurchaseId = p.PurchaseId,
                                      IsApprove = p.IsApprove,
                                  }).FirstOrDefaultAsync();
            if (purchase != null)
            {
                //List<PurchaseDetailVM> purchaseDetailVMs = new List<PurchaseDetailVM>();
                var purchasesDetailsList = await (from pd in db.PurchaseDetails
                                                  join p in db.Product on pd.ProductId equals p.ProductId
                                                  join s in db.Stores on pd.StoreId equals s.StoreId
                                                  where pd.PurchaseId == id
                                                  select new PurchaseDetailViewModel()
                                                  {
                                                      PurchaseDetailId = pd.PurchaseDetailId,
                                                      Quantity = pd.Quantity,
                                                      Price = pd.Price,
                                                      PurchaseId = pd.PurchaseId,
                                                      ProductId = pd.ProductId,
                                                      StoreId = pd.StoreId,
                                                      SubTotal = pd.SubTotal,
                                                      Vat = pd.Vat,
                                                      ProductName = p.Name,
                                                      StoreName = s.StoreName
                                                  }).ToListAsync();
                foreach (var item in purchasesDetailsList)
                {
                    grandTotal += item.SubTotal;
                }
                oPurchase = new PurchaseViewModel()
                {
                    PurchaseId = purchase.PurchaseId,
                    Description = purchase.Description,
                    GrandTotal = grandTotal,
                    IsApprove = purchase.IsApprove,
                    SupplierId = purchase.SupplierId,
                    PurchaseDetails = purchasesDetailsList
                };
            }
            //var purchaselist = mapper.Map<List<PurchaseVM>>(list);
            return oPurchase;
        }

        public async Task<PurchaseViewModel> CreateMaster(PurchaseViewModel purchaseViewModel)
        {
            try
            {
                #region Master insert/update
                var purchase = await db.Purchases.Where(x => x.PurchaseId == purchaseViewModel.PurchaseId).FirstOrDefaultAsync();
                if (purchase == null)
                {
                    purchase = new PurchaseModel
                    {
                        Description = purchaseViewModel.Description,
                        SupplierId = purchaseViewModel.SupplierId,
                        GrandTotal = purchaseViewModel.GrandTotal,
                        IsApprove = purchaseViewModel.IsApprove,
                    };
                    db.Purchases.Add(purchase);
                    await db.SaveChangesAsync();
                    purchaseViewModel.PurchaseId = purchase.PurchaseId;
                }
                else
                {
                    purchase.Description = purchaseViewModel.Description;
                    purchase.SupplierId = purchaseViewModel.SupplierId;
                    purchase.GrandTotal = purchaseViewModel.GrandTotal;
                    purchase.IsApprove = purchaseViewModel.IsApprove;
                    await db.SaveChangesAsync();
                }
                #endregion
                //return new OkResult();
                return purchaseViewModel;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                //return new BadRequestResult();
                return purchaseViewModel;
            }
        }

        public async Task<PurchaseDetailViewModel> CreateDetail(PurchaseDetailViewModel detail)
        {
            try
            {
                var purchaseDetail = new PurchaseDetailModel
                {
                    PurchaseDetailId = detail.PurchaseDetailId,
                    PurchaseId = detail.PurchaseId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    Vat = detail.Vat,
                    SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
                    StoreId = detail.StoreId
                };
                //{
                //    PurchaseId = detail.PurchaseId,
                //    ProductId = detail.ProductId,
                //    Quantity = detail.Quantity,
                //    Price = detail.Price,
                //    Vat = detail.Vat,
                //    SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
                //    StoreId = detail.StoreId
                //};
                db.PurchaseDetails.Add(purchaseDetail);
                await db.SaveChangesAsync();
                detail.PurchaseId = purchaseDetail.PurchaseId;
                detail.PurchaseDetailId = purchaseDetail.PurchaseDetailId;
                return detail;
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                //return new BadRequestResult();
                return detail;
            }
        }

        public Task<PurchaseViewModel> Approve(PurchaseViewModel purchaseViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseDetailViewModel> RemoveDetail(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<PurchaseViewModel> Approve(PurchaseViewModel purchaseViewModel)
        //{
        //    try
        //    {
        //        #region Insert Ledger + Update Stock
        //        foreach (var detail in purchaseViewModel.PurchaseDetails)
        //        {
        //            LedgerModel ledger = new LedgerModel();
        //            ledger.Price = detail.Price;
        //            ledger.Quantity = detail.Quantity;
        //            ledger.Price = detail.Price;
        //            ledger.InventoryTypeId = 1; // purchase
        //            ledger.StockTypeId = 2; // receive
        //            ledger.StoreId = detail.StoreId;
        //            ledger.ProductId = detail.ProductId;
        //            //ledger.UserId = Login UserID
        //            db.Ledgers.Add(ledger);
        //            await db.SaveChangesAsync();

        //            var oStock = (from x in db.Stocks
        //                          where x.ProductId == detail.ProductId && x.StoreId == detail.StoreId
        //                          select x).FirstOrDefault();
        //            if (oStock != null)
        //            {
        //                oStock.ProductId = detail.ProductId;
        //                oStock.StockQuantity += detail.Quantity;
        //                oStock.StoreId = detail.StoreId;
        //                db.SaveChanges();
        //            }
        //            else
        //            {
        //                StockModel stock = new StockModel();
        //                stock.ProductId = detail.ProductId;
        //                stock.StockQuantity = detail.Quantity;
        //                stock.StoreId = detail.StoreId;
        //                db.Add(stock);
        //                db.SaveChanges();
        //            }

        //            var purchase = (from x in db.Purchases
        //                            where x.PurchaseId == purchaseViewModel.PurchaseId
        //                            select x).FirstOrDefault();
        //            if (purchase != null)
        //            {
        //                purchase.IsApprove = true;
        //                db.SaveChanges();
        //            }
        //        }

        //        #endregion
        //        return purchaseViewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        var ErrorMessage = ex.Message;
        //        return purchaseViewModel;
        //    }
        //}

        //public async Task<PurchaseDetailViewModel> RemoveDetail(int detailID)
        //{
        //    PurchaseDetailViewModel purchaseDetailViewModel = new PurchaseDetailViewModel();
        //    try
        //    {
        //        var purchaseDetail = await db.PurchaseDetails.Where(x => x.PurchaseId == detailID).FirstOrDefaultAsync();
        //        if (purchaseDetail != null)
        //        {
        //            db.PurchaseDetails.Remove(purchaseDetail);
        //            await db.SaveChangesAsync();
        //            purchaseDetailViewModel.PurchaseDetailId = purchaseDetail.PurchaseDetailId;
        //            purchaseDetailViewModel.PurchaseId = purchaseDetail.PurchaseId;
        //        }
        //        return purchaseDetailViewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        var ErrorMessage = ex.Message;
        //        //return new BadRequestResult();
        //        return purchaseDetailViewModel;
        //    }
        //}

    }
}



//public async Task<IActionResult> Create(PurchaseVM purchaseVM)
//{
//    if (!purchaseVM.PurchaseDetails.Any())
//    {
//        return new BadRequestObjectResult("Please add at least one product.");
//    }

//    foreach (var detail in purchaseVM.PurchaseDetails)
//    {
//        if (detail.ProductId <= 0)
//            return new BadRequestObjectResult("Product is required in each row.");
//        if (detail.Price <= 0)
//            return new BadRequestObjectResult("Price must be greater than 0.");
//        if (detail.Quantity <= 0)
//            return new BadRequestObjectResult("Quantity must be at least 1.");
//    }

//    if (!purchaseVM.InventoryTypeId.HasValue || !purchaseVM.StockTypeId.HasValue)
//    {
//        return new BadRequestObjectResult("Inventory Type and Stock Type are required.");
//    }

//    purchaseVM.GrandTotal = purchaseVM.PurchaseDetails
//        .Sum(x => (x.Price * x.Quantity) + x.Vat);

//    using var transaction = await db.Database.BeginTransactionAsync();
//    try
//    {
//        var purchase = new Purchase
//        {
//            PurchaseId = purchaseVM.PurchaseId,
//            Description = purchaseVM.Description,
//            SupplierId = purchaseVM.SupplierId,
//            GrandTotal = purchaseVM.GrandTotal,
//            IsApprove = purchaseVM.IsApprove
//        };

//        db.Purchases.Add(purchase);
//        await db.SaveChangesAsync();

//        foreach (var detail in purchaseVM.PurchaseDetails)
//        {
//            var purchaseDetail = new PurchaseDetail
//            {
//                PurchaseDetailId = detail.PurchaseDetailId,
//                PurchaseId = purchase.PurchaseId,
//                ProductId = detail.ProductId,
//                Quantity = detail.Quantity,
//                Price = detail.Price,
//                Vat = detail.Vat,
//                SubTotal = (detail.Quantity * detail.Price) + detail.Vat,
//                StoreId = detail.StoreId
//            };

//            db.PurchasesDetails.Add(purchaseDetail);
//        }

//        await db.SaveChangesAsync();

//        if (purchaseVM.IsApprove)
//        {
//            var purchaseDetails = await db.PurchasesDetails
//                .Where(x => x.PurchaseId == purchase.PurchaseId)
//                .ToListAsync();

//            foreach (var detail in purchaseDetails)
//            {
//                var ledger = new Ledger
//                {
//                    LedgerId = detail.StoreId,
//                    ProductId = detail.ProductId,
//                    Quantity = detail.Quantity,
//                    InventoryTypeId = 1,  // Set InventoryType = 1
//                    StockTypeId = 1,      // Set StockType = 1
//                    UserId = null            // Assume a default UserId (or pass the actual user id)
//                };

//                db.Ledgers.Add(ledger);
//                await db.SaveChangesAsync();

//                detail.StoreId = ledger.LedgerId;
//                db.PurchasesDetails.Update(detail);

//                if (ledger.InventoryTypeId == 1 && ledger.StockTypeId == 1)
//                {
//                    var stock = await db.Stocks
//                        .FirstOrDefaultAsync(s => s.ProductId == detail.ProductId && s.S == 1);

//                    if (stock == null)
//                    {
//                        stock = new Stock
//                        {
//                            StockId = detail.ProductId,
//                            ProductId = detail.ProductId,
//                            LedgerId = ledger.LedgerId,
//                            Quantity = detail.Quantity,
//                            StockTypeId = 1
//                        };
//                        db.Stocks.Add(stock);
//                    }
//                    else
//                    {
//                        stock.Quantity += detail.Quantity;
//                        db.Stocks.Update(stock);
//                    }
//                }
//            }

//            await db.SaveChangesAsync();
//        }

//        await transaction.CommitAsync();
//        return new OkObjectResult(new { message = purchaseVM.IsApprove ? "Purchase created and approved." : "Purchase created (pending approval)." });
//    }
//    catch (Exception ex)
//    {
//        await transaction.RollbackAsync();
//        return new JsonResult(new { error = ex.Message });
//    }
//}
