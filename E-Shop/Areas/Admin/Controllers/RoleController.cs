using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRole role;
        public RoleController(IRole role)
        {
            this.role = role;
        }
        public async Task<IActionResult> Index()
        {
            var rolelist = await role.GetAll();
            return View(rolelist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            return View(roleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await role.Create(roleViewModel);
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allrole = await role.GetAll();
            var roleViewModel = allrole.FirstOrDefault(s => s.RoleId == id);

            if (roleViewModel == null)
            {
                return NotFound();
            }
            return View(roleViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await role.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var roleDetails = await role.GetById(id);

            if (roleDetails == null)
            {
                return NotFound();
            }

            return View(roleDetails);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await role.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var updaterole = await role.Update(roleViewModel);
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }
    }
}
