using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class UserRepo: IUser
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public UserRepo(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<List<UserViewModel>> GetAll()
        {
            var list = await db.Users.ToListAsync();
            var userlist = mapper.Map<List<UserViewModel>>(list);
            return userlist;
        }
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            try
            {
                UserModel users = new UserModel
                {
                    UserId = userViewModel.UserId,
                    UserName = userViewModel.UserName,
                    Mobile = userViewModel.Mobile,
                    Address = userViewModel.Address,
                    Email = userViewModel.Email,
                    IsActive = userViewModel.IsActive
                };
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return new JsonResult(new { success = true, message = "Supplier created successfully!" });
            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                return new JsonResult(ErrorMessage);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userid = db.Users.Where(sid => sid.UserId == id).FirstOrDefault();
            if (userid != null)
            {
                db.Users.Remove(userid);
                await db.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestResult();

        }
        public async Task<IActionResult> Update(UserViewModel userViewModel)
        {
            var useredit = await db.Users.FirstOrDefaultAsync(a => a.UserId == userViewModel.UserId);
            if (useredit == null)
            {
                if (useredit == null)
                    return new NotFoundResult();
            }
            useredit.UserId = userViewModel.UserId;
            useredit.UserName = userViewModel.UserName;
            useredit.Mobile = userViewModel.Mobile;
            useredit.Address = userViewModel.Address;
            useredit.IsActive = userViewModel.IsActive;
            db.Users.Update(useredit);
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<UserViewModel> GetById(int id)
        {
            var userId = await db.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
            var data = mapper.Map<UserViewModel>(userId);
            return data;
        }
    }

}

