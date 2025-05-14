using System.Collections;
using AutoMapper;
using E_Shop.Data;
using E_Shop.Models;
using E_Shop.Service.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Service.Repository
{
    public class InventoryTypeRopo : IInventoryType
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public InventoryTypeRopo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<InventoryTypeViewModel>> GetAll()
        {
            var list = await db.InventoryTypes.ToArrayAsync();
            var datalist = mapper.Map<List<InventoryTypeViewModel>>(list);
            return datalist;
        }
        public async Task<IActionResult> Create(InventoryTypeViewModel inventoryTypeViewModel)
        {
            try
            {
                InventoryTypeModel inventoryType = new InventoryTypeModel
                {
                    InventoryTypeId = inventoryTypeViewModel.InventoryTypeId,
                    Name = inventoryTypeViewModel.Name,
                    Remarks = inventoryTypeViewModel.Remarks
                };
                db.InventoryTypes.Add(inventoryType);   
                await db.SaveChangesAsync();
                return new JsonResult(new {success=true, message="InventoryType Created Successfully"});
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }

        public async Task<IList<InventoryTypeModel>> GetAllInventoryType()
        {
          return await  db.InventoryTypes.ToListAsync();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var roleid = db.InventoryTypes.Where(sid => sid.InventoryTypeId == id).FirstOrDefault();
            if (roleid != null)
            {
                db.InventoryTypes.Remove(roleid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(InventoryTypeViewModel roleViewModel)
        {
            var roleid = await db.InventoryTypes.FirstOrDefaultAsync(a => a.InventoryTypeId == roleViewModel.InventoryTypeId);
            if (roleid == null)
            {
                return new NotFoundResult();
            }
            roleid.InventoryTypeId = roleViewModel.InventoryTypeId;
            roleid.Name = roleViewModel.Name;
            roleid.Remarks = roleViewModel.Remarks;
            db.InventoryTypes.Update(roleid);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<InventoryTypeViewModel> GetById(int id)
        {
            var roleId = await db.InventoryTypes.Where(x => x.InventoryTypeId == id).FirstOrDefaultAsync();
            var data = mapper.Map<InventoryTypeViewModel>(roleId);
            return data;
        }

        Task<IList<InventoryTypeViewModel>> IInventoryType.GetAllInventoryType()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
