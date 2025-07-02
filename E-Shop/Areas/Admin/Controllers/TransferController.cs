using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModel;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransferController : Controller
    {
        private readonly ITransfer _transfer;
        private readonly IProduct _product;
        private readonly IStore _store;

        public TransferController(ITransfer transfer, IProduct product, IStore store)
        {
            _transfer = transfer;
            _product = product;
            _store = store;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _transfer.GetAll();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var transferVM = await _transfer.GetById(id.Value);
            if (transferVM != null)
            {
                await _transfer.Approve(transferVM);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            await LoadDropdowns();

            var transferVM = new TransferViewModel();
            if (id != null)
            {
                var transfer = await _transfer.GetById(id.Value);
                if (transfer != null)
                    transferVM = transfer;
            }

            return View(transferVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please correct the errors and try again.";
                await LoadDropdowns();
                return View(model);
            }

            if (model.IsApprove)
            {
                ViewBag.ErrorMessage = "Approved transfer cannot be modified.";
                await LoadDropdowns();
                return View(model);
            }

            try
            {
                var transfer = await _transfer.CreateMaster(model);

                if (model.TransferDetails != null)
                {
                    foreach (var detail in model.TransferDetails)
                    {
                        detail.TransferId = transfer.TransferId;
                        await _transfer.CreateDetail(detail);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.ErrorMessage = $"Error occurred while saving the transfer: {ex.Message}";
                await LoadDropdowns();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetail(TransferDetailViewModel detail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _transfer.CreateDetail(detail);
                    return RedirectToAction("Create", new { id = detail.TransferId });
                }
                catch (System.Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error occurred while saving the detail: {ex.Message}";
                }
            }

            return RedirectToAction("Create", new { id = detail.TransferId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveDetail(int id)
        {
            try
            {
                await _transfer.RemoveDetail(id);
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.ErrorMessage = $"Error occurred while removing the detail: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        private async Task LoadDropdowns()
        {
            var productList = await _product.GetAllProducts() ?? new List<ProductModel>();
            var storeList = await _store.GetAllStores() ?? new List<StoreModel>();

            ViewBag.Products = productList.Select(p => new SelectListItem
            {
                Value = p.ProductId.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Stores = storeList.Select(s => new SelectListItem
            {
                Value = s.StoreId.ToString(),
                Text = s.StoreName
            }).ToList();
        }
    }
}
