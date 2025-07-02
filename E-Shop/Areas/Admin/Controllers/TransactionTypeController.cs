using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    using E_Shop.Models.Admin;
    using E_Shop.Services.Interface;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    [Area("Admin")]

    public class TransactionTypeController : Controller
    {
        private readonly ITransaction _repository;

        public TransactionTypeController(ITransaction repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var transactionTypes = await _repository.GetAllAsync();
            return View(transactionTypes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var transactionType = await _repository.GetByIdAsync(id);
            if (transactionType == null) return NotFound();
            return View(transactionType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionTypeModel model)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var transactionType = await _repository.GetByIdAsync(id);
            if (transactionType == null) return NotFound();
            return View(transactionType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransactionTypeModel model)
        {
            if (id != model.TransactionTypeId) return BadRequest();
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var transactionType = await _repository.GetByIdAsync(id);
            if (transactionType == null) return NotFound();
            return View(transactionType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
