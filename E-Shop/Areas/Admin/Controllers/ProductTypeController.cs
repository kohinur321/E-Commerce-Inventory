using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
    [Area("Admin")]
    public class ProductTypeController : Controller
    {
        private readonly IProductType _productTypeService;

        public ProductTypeController(IProductType productTypeService)
        {
            _productTypeService = productTypeService;
        }

        // GET: ProductType
        public async Task<IActionResult> Index()
        {
            var productTypes = await _productTypeService.GetAll();
            return View(productTypes);
        }

        // GET: ProductType/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _productTypeService.GetById(id);
            if (model == null)
                return NotFound();

            return View(model);
        }


        // GET: ProductType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productTypeService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ProductType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _productTypeService.GetById(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        // POST: ProductType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productTypeService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ProductType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productTypeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
