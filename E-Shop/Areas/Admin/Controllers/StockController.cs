using E_Shop.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
    [Area("Admin")]
    public class StockController : Controller
    {
        private readonly IStock _stockRepo;

        public StockController(IStock stockRepo)
        {
            _stockRepo = stockRepo;
        }

        // GET: /Stock/
        public async Task<IActionResult> Index()
        {
            var stocks = await _stockRepo.GetAllAsync();
            return View(stocks);
        }

        // GET: /Stock/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
                return NotFound();

            return View(stock);
        }

        // GET: /Stock/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockModel stock)
        {
            if (ModelState.IsValid)
            {
                await _stockRepo.AddAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        // GET: /Stock/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
                return NotFound();

            return View(stock);
        }

        // POST: /Stock/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockModel stock)
        {
            if (ModelState.IsValid)
            {
                await _stockRepo.UpdateAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        // GET: /Stock/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
                return NotFound();

            return View(stock);
        }

        // POST: /Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _stockRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
