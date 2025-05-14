using E_Shop.Services.Interface;
using E_Shop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IProduct _product;
        private readonly ICart _cart;
        public HomeController(IProduct product,ICart cart)
        {
            _product = product;
            _cart = cart;
        }
        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var list = await _product.GetAll();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await _product.GetById(id);
            return View(data);
        }

        /// <summary>
        ///  Buy to data Save in Cart
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(CartViewModel model)
        {
            var data = await _cart.Create(model);
            return new JsonResult(data);
        }

        /// <summary>
        /// Get Data in Cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBuyItem()
        {
            var data = await _cart.GetAll();
            return new JsonResult(data);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _cart.Delete(id);
            return new JsonResult(data);
        }
    }
}
