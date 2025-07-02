using Microsoft.AspNetCore.Mvc;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;

namespace E_Shop.Controllers
{
    [Area("Admin")]
    public class DamageDetailsController : Controller
    {
        private readonly IDamageDetails _damageDetailsService;

        public DamageDetailsController(IDamageDetails damageDetailsService)
        {
            _damageDetailsService = damageDetailsService;
        }

        // GET: DamageDetails
        public async Task<IActionResult> Index()
        {
            var damageDetails = await _damageDetailsService.GetAllAsync();
            return View(damageDetails);
        }

        // GET: DamageDetails/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var damageDetail = await _damageDetailsService.GetByIdAsync(id);
            if (damageDetail == null)
            {
                return NotFound();
            }
            return View(damageDetail);
        }

        // GET: DamageDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DamageDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DamageDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _damageDetailsService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: DamageDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var damageDetail = await _damageDetailsService.GetByIdAsync(id);
            if (damageDetail == null)
            {
                return NotFound();
            }
            return View(damageDetail);
        }

        // POST: DamageDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DamageDetailViewModel model)
        {
            if (id != model.DamageDetailId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _damageDetailsService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: DamageDetails/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var damageDetail = await _damageDetailsService.GetByIdAsync(id);
            if (damageDetail == null)
            {
                return NotFound();
            }
            return View(damageDetail);
        }

        // POST: DamageDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _damageDetailsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
