using E_Shop.Data;
using E_Shop.Migrations;
using E_Shop.Models;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class UserRoleRepo : IUserRole
    {
        private readonly ApplicationDbContext db;

        public UserRoleRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<List<UserRoleViewModel>> GetAll()
        {
            return await db.UserRoles
                .Include(ur => ur.UserModel)
                .Include(ur => ur.RoleModel)
                .Select(ur => new UserRoleViewModel
                {
                    UserRoleId = ur.UserRoleId,
                    UserId = ur.UserId,
                    UserName = ur.UserModel.UserName,
                    RoleId = ur.RoleId,
                    RoleName = ur.RoleModel.RoleName
                }).ToListAsync();
        }

        public async Task<UserRoleViewModel> GetById(int id)
        {
            var ur = await db.UserRoles
                .Include(u => u.UserModel)
                .Include(r => r.RoleModel)
                .FirstOrDefaultAsync(x => x.UserRoleId == id);

            if (ur == null) return null;

            return new UserRoleViewModel
            {
                UserRoleId = ur.UserRoleId,
                UserId = ur.UserId,
                UserName = ur.UserModel.UserName,
                RoleId = ur.RoleId,
                RoleName = ur.RoleModel.RoleName
            };
        }

        public async Task<bool> Create(UserRoleViewModel model)
        {
            var entity = new UserRoleModel
            {
                UserId = model.UserId,
                RoleId = model.RoleId
            };

            db.UserRoles.Add(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(UserRoleViewModel model)
        {
            var entity = await db.UserRoles.FindAsync(model.UserRoleId);
            if (entity == null) return false;

            entity.UserId = model.UserId;
            entity.RoleId = model.RoleId;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await db.UserRoles.FindAsync(id);
            if (entity == null) return false;

            db.UserRoles.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
