using E_Shop.Migrations;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseController : Controller
    {
        private readonly IPurchase _purchase;
        private readonly ISupplier _supplier;
        private readonly IProduct _product;
        private readonly IStore _store;

        public PurchaseController(IPurchase purchase, ISupplier supplier, IProduct product, IStore store)
        {
            _purchase = purchase;
            _supplier = supplier;
            _product = product;
            _store = store;
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int? id)
        {
            try
            {

                var purchaseViewModel = new PurchaseViewModel();
                if (id != null)
                {
                    var purchase = await _purchase.GetById((int)id);
                    if (purchase != null)
                    {
                        purchaseViewModel = purchase;
                        await _purchase.Approve(purchaseViewModel);
                    }
                }
                //purchaseVM.Suppliers = suppliers;
                //purchaseVM.Products = products;
                //purchaseVM.Stores = stores;
                //purchaseVM.Suppliers = suppliers;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            try
            {
                var supplierList = await _supplier.GetAllSupplier() ?? new List<SupplierModel>();
                var productList = await _product.GetAllProducts() ?? new List<ProductModel>();
                var storeList = await _store.GetAllStores() ?? new List<StoreModel>();

                var suppliers = new List<SelectListItem>();
                foreach (var item in supplierList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.SupplierId.ToString();
                    selec.Text = item.SupplierName;
                    suppliers.Add(selec);
                }
                var products = new List<SelectListItem>();
                foreach (var item in productList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.ProductId.ToString();
                    selec.Text = item.Name;
                    products.Add(selec);
                }
                var stores = new List<SelectListItem>();
                foreach (var item in storeList)
                {
                    var selec = new SelectListItem();
                    selec.Value = item.StoreId.ToString();
                    selec.Text = item.StoreName;
                    stores.Add(selec);
                }

                var purchaseVM = new PurchaseViewModel();
                if (id != null)
                {
                    var purchase = await _purchase.GetById((int)id);
                    if (purchase != null)
                    {
                        purchaseVM = purchase;
                    }
                }
                purchaseVM.Suppliers = suppliers;
                purchaseVM.Products = products;
                purchaseVM.Stores = stores;
                return View(purchaseVM);
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseViewModel purchaseVM)
        {
            if (ModelState.IsValid)
            {
                if (purchaseVM.IsApprove)
                {
                    ViewBag.ErrorMessage = "Model is not allowed to modify.";
                }
                else
                {
                    try
                    {
                        var purchase = await _purchase.CreateMaster(purchaseVM);
                        var purchaseDetailVM = new PurchaseDetailViewModel();
                        purchaseDetailVM.Quantity = purchaseVM.Quantity;
                        purchaseDetailVM.Price = purchaseVM.Price;
                        purchaseDetailVM.PurchaseId = purchase.PurchaseId;
                        purchaseDetailVM.ProductId = purchaseVM.ProductId;
                        purchaseDetailVM.StoreId = purchaseVM.StoreId;
                        purchaseDetailVM.SubTotal = purchaseVM.SubTotal;
                        purchaseDetailVM.Vat = purchaseVM.Vat;
                        await _purchase.CreateDetail(purchaseDetailVM);
                        return RedirectToAction("Create", new { id = purchase.PurchaseId });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Model not valid.";
            }

            if (purchaseVM.PurchaseId > 0)
            {
                purchaseVM = await _purchase.GetById(purchaseVM.PurchaseId);
            }

            var supplierList = await _supplier.GetAllSupplier() ?? new List<SupplierModel>();
            var productList = await _product.GetAllProducts() ?? new List<ProductModel>();
            var storeList = await _store.GetAllStores() ?? new List<StoreModel>();
            var suppliers = new List<SelectListItem>();
            foreach (var item in supplierList)
            {
                var selec = new SelectListItem();
                selec.Value = item.SupplierId.ToString();
                selec.Text = item.SupplierName;
                suppliers.Add(selec);
            }
            var products = new List<SelectListItem>();
            foreach (var item in productList)
            {
                var selec = new SelectListItem();
                selec.Value = item.ProductId.ToString();
                selec.Text = item.Name;
                products.Add(selec);
            }
            var stores = new List<SelectListItem>();
            foreach (var item in storeList)
            {
                var selec = new SelectListItem();
                selec.Value = item.StoreId.ToString();
                selec.Text = item.StoreName;
                stores.Add(selec);
            }
            purchaseVM.Suppliers = suppliers;
            purchaseVM.Products = products;
            purchaseVM.Stores = stores;
            return View(purchaseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetail(PurchaseDetailViewModel purchaseDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _purchase.CreateDetail(purchaseDetailViewModel);
                    return RedirectToAction("Create", new { id = purchaseDetailViewModel.PurchaseId });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
                }
            }
            return RedirectToAction("Create", new { id = purchaseDetailViewModel.PurchaseId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveDetail(int id)
        {
            try
            {
                var purchaseDetailVM = await _purchase.RemoveDetail(id);
                return RedirectToAction("Create", new { id = purchaseDetailVM.PurchaseId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error occurred while saving the purchase: {ex.Message}";
            }
            return RedirectToAction("Create", new { id = 0 });
        }

        public async Task<IActionResult> Index()
        {
            var datalist = await _purchase.GetAll();
            return View(datalist);
        }

    }
}
