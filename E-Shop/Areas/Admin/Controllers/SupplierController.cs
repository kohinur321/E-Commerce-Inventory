using E_Shop.Data;
using E_Shop.Services.Interface;
using E_Shop.ViewModel;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly ISupplier _supplier;

        public SupplierController(ISupplier supplier)
        {
            _supplier = supplier;
        }

        /// <summary>
        /// Supplier List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _supplier.GetAll();
            return View(data);
        }

        /// <summary>
        /// Create Form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            SupplierViewModel data = new SupplierViewModel();
            return View(data);
        }

        /// <summary>
        /// Create Supplier
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(SupplierViewModel model)
        {
            try
            {
                var status = await _supplier.Create(model);

                if (status.StatusCode == 1)
                {
                    TempData["SuccessMessage"] = status.Message;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = status.Message;
                    ModelState.AddModelError("", status.Message);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                ModelState.AddModelError("", "An error occurred");
                return View(model);
            }
        }

        /// <summary>
        /// Get Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _supplier.GetById(id);
            return View(data);
        }

        /// <summary>
        /// Edit Supplier
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(SupplierViewModel model)
        {
            try
            {
                var status = await _supplier.Update(model);

                if (status.StatusCode == 1)
                {
                    TempData["SuccessMessage"] = status.Message;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = status.Message;
                    ModelState.AddModelError("", status.Message);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                ModelState.AddModelError("", "An error occurred");
                return View(model);
            }
        }

        /// <summary>
        /// Delete Supplier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _supplier.Delete(id);
            if (status.StatusCode == 1)
            {
                TempData["SuccessMessage"] = status.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = status.Message;
                ModelState.AddModelError("", status.Message);
                return View();
            }
        }

        /// <summary>
        /// Supplier Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await _supplier.GetById(id);
            return View(data);
        }
    }
}
