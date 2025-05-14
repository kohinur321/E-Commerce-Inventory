using Microsoft.AspNetCore.Mvc;
using E_Shop.Service.Interface;
using E_Shop.ViewModels;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockTypeController : Controller
    {
        private readonly IStockType stockType;
        public StockTypeController(IStockType stockType)
        {
            this.stockType = stockType;
        }
        public async Task<IActionResult> Index()
        {
            var typelist = await stockType.GetAll();
            return View(typelist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            StockTypeViewModel stockTypeViewModel = new StockTypeViewModel();
            return View(stockTypeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StockTypeViewModel stockTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await stockType.Create(stockTypeViewModel);
                return RedirectToAction("Index");
            }
            return View(stockType);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allSuppliers = await stockType.GetAll();
            var supplierViewModel = allSuppliers.FirstOrDefault(s => s.StockTypeId == id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }
            return View(supplierViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await stockType.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await stockType.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StockTypeViewModel stockTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var updatesupplier = await stockType.Update(stockTypeViewModel);
                return RedirectToAction("Index");
            }
            return View(stockTypeViewModel);
        }
    }
}
