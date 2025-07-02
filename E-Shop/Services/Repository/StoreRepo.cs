using System.Collections;
using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class StoreRepo : IStore
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StoreRepo(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create(StoreViewModel storeViewModel)
        {
            try
            {
                var store = _mapper.Map<StoreModel>(storeViewModel);
                _db.Stores.Add(store);
                await _db.SaveChangesAsync();
                return new JsonResult(new { status = "success", message = "Store created successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = "error", message = ex.Message });
            }
        }

        public async Task<List<StoreViewModel>> GetAll()
        {
            try
            {
                var stores = await _db.Stores.ToListAsync();
                return _mapper.Map<List<StoreViewModel>>(stores);
            }
            catch (Exception ex)
            {
                // Debug এ দেখার জন্য অথবা log করার জন্য
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<StoreViewModel> GetById(int id)
        {
            var store = await _db.Stores.FindAsync(id);
            return _mapper.Map<StoreViewModel>(store);
        }

        public async Task<IActionResult> Update(StoreViewModel storeViewModel)
        {
            try
            {
                var store = await _db.Stores.FindAsync(storeViewModel.StoreId);
                if (store == null) return new JsonResult(new { status = "error", message = "Store not found" });

                _mapper.Map(storeViewModel, store);
                await _db.SaveChangesAsync();
                return new JsonResult(new { status = "success", message = "Store updated successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = "error", message = ex.Message });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var store = await _db.Stores.FindAsync(id);
                if (store == null) return new JsonResult(new { status = "error", message = "Store not found" });

                _db.Stores.Remove(store);
                await _db.SaveChangesAsync();
                return new JsonResult(new { status = "success", message = "Store deleted successfully" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = "error", message = ex.Message });
            }
        }

        public Task<IEnumerable> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<StoreModel>> GetAllStores()
        {
            return await _db.Stores.ToListAsync();
        }
    }
}
