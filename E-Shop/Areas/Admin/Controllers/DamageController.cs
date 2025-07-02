using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
    [Area("Admin")]
    public class DamageController : Controller
    {
        private readonly IDamage _damageService;

        public DamageController(IDamage damageService)
        {
            _damageService = damageService;
        }

        // GET: /Damage/
        public async Task<IActionResult> Index()
        {
            var damages = await _damageService.GetAllDamagesAsync();
            return View(damages);
        }

        // GET: /Damage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var damage = await _damageService.GetDamageByIdAsync(id);
            if (damage == null)
                return NotFound();

            return View(damage);
        }

        // GET: /Damage/Create
        public IActionResult Create()
        {
            return View(new DamageViewModel());
        }

        // POST: /Damage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DamageViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _damageService.CreateDamageAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Damage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var damage = await _damageService.GetDamageByIdAsync(id);
            if (damage == null)
                return NotFound();

            return View(damage);
        }

        // POST: /Damage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DamageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var updated = await _damageService.UpdateDamageAsync(model);
                if (!updated)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Damage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var damage = await _damageService.GetDamageByIdAsync(id);
            if (damage == null)
                return NotFound();

            return View(damage);
        }

        // POST: /Damage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _damageService.DeleteDamageAsync(id);
            if (!deleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // POST: /Damage/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var approved = await _damageService.ApproveDamageAsync(id);
            if (!approved)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
