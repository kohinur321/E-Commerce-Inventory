using E_Shop.Services.Interface;
using E_Shop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        private readonly ICart _cart;
        private readonly ICustomer _customer;

        public OrderController(ICart cart, ICustomer customer)
        {
            _cart = cart;
            _customer = customer;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get all Cart Items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCartItem()
        {
            var data = await _cart.GetAll();
            return new JsonResult(data);
        }
        /// <summary>
        /// Save Order complete
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(CustomerViewModel model)
        {
            var data = await _customer.Save(model);
            return new JsonResult(data);
        }
    }
}
