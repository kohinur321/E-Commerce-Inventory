using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoreController : Controller
    {
        private readonly IStore _storeService;

        public StoreController(IStore storeService)
        {
            _storeService = storeService;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _storeService.GetAll();
            return View(stores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreViewModel storeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _storeService.Create(storeViewModel);
                return RedirectToAction("Index");
            }
            return View(storeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _storeService.GetById(id);
            if (store == null) return NotFound();
            return View(store);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StoreViewModel storeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _storeService.Update(storeViewModel);
                return RedirectToAction("Index");
            }
            return View(storeViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _storeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
