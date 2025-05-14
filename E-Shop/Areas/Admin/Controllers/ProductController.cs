using E_Shop.Data;
using E_Shop.Services.Interface;
using E_Shop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }
        /// <summary>
        /// Product List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _product.GetAll();
            return View(data);
        }

        /// <summary>
        /// Create Form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel data = new ProductViewModel();
            return View(data);
        }

        /// <summary>
        /// Create Product Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            try
            {
                var status = await _product.Create(model);

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
            var data = await _product.GetById(id);
            return View(data);
        }
        /// <summary>
        /// Edit Post Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            try
            {
                var status = await _product.Update(model);

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
        /// GetDelete 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _product.Delete(id);
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
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await _product.GetById(id);
            return View(data);
        }
    }
}
