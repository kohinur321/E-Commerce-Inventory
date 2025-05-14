using E_Shop.Service.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    [Area("Admin")]
    public class InventoryTypeController : Controller
    {
        private readonly IInventoryType inventoryType;
        public InventoryTypeController(IInventoryType inventoryType)
        {
            this.inventoryType = inventoryType;
        }

        public async Task<IActionResult> Index()
        {
            var list = await inventoryType.GetAll();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            InventoryTypeViewModel inventoryTypeViewModel = new InventoryTypeViewModel();
            return View(inventoryTypeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(InventoryTypeViewModel inventoryTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                await inventoryType.Create(inventoryTypeViewModel);
                return RedirectToAction("Index");
            }
            return View(inventoryTypeViewModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allrole = await inventoryType.GetAll();
            var roleVM = allrole.FirstOrDefault(s => s.InventoryTypeId == id);

            if (roleVM == null)
            {
                return NotFound();
            }
            return View(roleVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await inventoryType.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await inventoryType.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(InventoryTypeViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleViewModel);
            }

            var existingItem = await inventoryType.GetById(roleViewModel.InventoryTypeId);
            if (existingItem == null)
            {
                return NotFound();
            }

            var updaterole = await inventoryType.Update(roleViewModel);
            if (updaterole is OkResult)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}
