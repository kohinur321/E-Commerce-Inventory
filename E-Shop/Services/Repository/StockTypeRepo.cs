using AutoMapper;
using E_Shop.Data;
using E_Shop.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Shop.Models;
using E_Shop.Service.Interface;
using E_Shop.ViewModels;
using System.Collections;

namespace VehicleWorkShop.Service.Repository
{
    public class StockTypeRepo: IStockType
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public StockTypeRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<StockTypeViewModel>> GetAll()
        {
            var list = await db.StockTypes.ToArrayAsync();
            var Typlist = mapper.Map<List<StockTypeViewModel>>(list);
            return Typlist;
        }
        public async Task<IActionResult> Create(StockTypeViewModel stockTypeViewModel)
        {
            try
            {
                StockTypeModel stockType = new StockTypeModel
                {
                    StockTypeName = stockTypeViewModel.StockTypeName,
                };
                db.StockTypes.Add(stockType);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<IList<StockTypeModel>> GetAllStockType()
        {
           return await db.StockTypes.ToListAsync();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var supplierid = db.StockTypes.Where(sid => sid.StockTypeId == id).FirstOrDefault();
            if (supplierid != null)
            {
                db.StockTypes.Remove(supplierid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(StockTypeViewModel supplierViewModel)
        {
            var supplieredit = await db.StockTypes.FirstOrDefaultAsync(a => a.StockTypeId == supplierViewModel.StockTypeId);
            if (supplieredit == null)
            {
                    return new NotFoundResult();
            }
            supplieredit.StockTypeId = supplierViewModel.StockTypeId;
            supplieredit.StockTypeName = supplierViewModel.StockTypeName;
            db.StockTypes.Update(supplieredit);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<StockTypeViewModel> GetById(int id)
        {
            var supplierId = await db.StockTypes.Where(x => x.StockTypeId == id).FirstOrDefaultAsync();
            var data = mapper.Map<StockTypeViewModel>(supplierId);
            return data;
        }

        public Task<IEnumerable> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
