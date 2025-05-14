using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRoleController : Controller
    {
        private readonly IUserRole userRoleService;
        private readonly ApplicationDbContext db;

        public UserRoleController(IUserRole userRoleService, ApplicationDbContext db)
        {
            this.userRoleService = userRoleService;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var data = await userRoleService.GetAll();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new UserRoleViewModel
            {
                Users = await db.Users.Select(u => new SelectListItem { Value = u.UserId.ToString(), Text = u.UserName }).ToListAsync(),
                Roles = await db.Roles.Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName }).ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await userRoleService.Create(model);
                return RedirectToAction("Index");
            }

            model.UserModel = await db.Users.Select(u => new SelectListItem { Value = u.UserId.ToString(), Text = u.UserName }).ToListAsync();
            model.Roles = await db.Roles.Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName }).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await userRoleService.GetById(id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await userRoleService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await userRoleService.GetById(id);
            if (data == null)
            {
                return NotFound();
            }

            // DropDown list পূরণ করো
            data.Users = await db.Users
                .Select(u => new SelectListItem { Value = u.UserId.ToString(), Text = u.UserName })
                .ToListAsync();

            data.Roles = await db.Roles
                .Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName })
                .ToListAsync();

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await userRoleService.Update(model);
                if (success)
                {
                    return RedirectToAction("Index");
                }
            }

            // পুনরায় ড্রপডাউন ডেটা পাঠাও
            model.Users = await db.Users
                .Select(u => new SelectListItem { Value = u.UserId.ToString(), Text = u.UserName })
                .ToListAsync();

            model.Roles = await db.Roles
                .Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName })
                .ToListAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await userRoleService.GetById(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

    }
}
