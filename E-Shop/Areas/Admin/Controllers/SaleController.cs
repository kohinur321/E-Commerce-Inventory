using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Shop.Services.Interface;
using E_Shop.Data;
using E_Shop.ViewModels;
using E_Shop.ViewModels.Admin;

namespace E_Shop.Controllers.Admin
{
    [Area("Admin")]
    public class SaleController : Controller
    {
        private readonly ISale sale;
        private readonly ICustomer customer;
        private readonly IProduct product;
        private readonly IStore store;
        private readonly ApplicationDbContext _context;

        public SaleController(
            ISale sale,
            ICustomer customer,
            IProduct product,
            IStore store,
            ApplicationDbContext context)
        {
            this.sale = sale;
            this.customer = customer;
            this.product = product;
            this.store = store;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await sale.GetAll();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSelectLists(); // ViewBag গুলো লোড করো

            return View(new SaleViewModel
            {
                SaleDetailsViewModels = new List<SaleDetailsViewModel>
                {
                    new SaleDetailsViewModel()
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleViewModel saleVM)
        {
            if (ModelState.IsValid)
            {
                var result = await sale.Create(saleVM);
                if (result is OkResult)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Something went wrong while saving.");
            }

            // Fallback: Ensure SaleDetailsViewModels not null
            if (saleVM.SaleDetailsViewModels == null || saleVM.SaleDetailsViewModels.Count == 0)
            {
                saleVM.SaleDetailsViewModels = new List<SaleDetailsViewModel>
                {
                    new SaleDetailsViewModel()
                };
            }

            await LoadSelectLists(); // ViewBag গুলো পুনরায় লোড করো

            return View(saleVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await sale.Delete(id);
            if (result is OkResult)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }

        /// <summary>
        /// ViewBag এর জন্য সব Dropdown list লোড করার Helper Method
        /// </summary>
        private async Task LoadSelectLists()
        {
            var customers = _context.Customer?.ToList() ?? new List<E_Shop.Models.User.CustomerModel>();
            var products = await product.GetAllProducts() ?? new List<E_Shop.Models.Admin.ProductModel>();
            var stores = await store.GetAllStores() ?? new List<E_Shop.Models.Admin.StoreModel>();

            ViewBag.Customers = new SelectList(customers, "CustomerId", "Name");
            ViewBag.Products = new SelectList(products, "ProductId", "Name");
            ViewBag.Stores = new SelectList(stores, "StoreId", "Name");
        }
    }
}
