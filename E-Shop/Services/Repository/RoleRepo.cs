using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class RoleRepo : IRole
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public RoleRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<RoleViewModel>> GetAll()
        {
            var list = await db.Roles.ToListAsync();
            var rolelist = mapper.Map<List<RoleViewModel>>(list);
            return rolelist;
        }
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            try
            {
                RoleModel role = new RoleModel
                {
                    RoleId = roleVM.RoleId,
                    RoleName = roleVM.RoleName,
                };
                db.Roles.Add(role);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Workshop created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var roleid = db.Roles.Where(sid => sid.RoleId == id).FirstOrDefault();
            if (roleid != null)
            {
                db.Roles.Remove(roleid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(RoleViewModel roleViewModel)
        {
            var roleid = await db.Roles.FirstOrDefaultAsync(a => a.RoleId == roleViewModel.RoleId);
            if (roleid == null)
            {
                return new NotFoundResult();
            }
            roleid.RoleId = roleViewModel.RoleId;
            roleid.RoleName = roleViewModel.RoleName;
            db.Roles.Update(roleid);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<RoleViewModel> GetById(int id)
        {
            var roleId = await db.Roles.Where(x => x.RoleId == id).FirstOrDefaultAsync();
            var data = mapper.Map<RoleViewModel>(roleId);
            return data;
        }
    }
}
