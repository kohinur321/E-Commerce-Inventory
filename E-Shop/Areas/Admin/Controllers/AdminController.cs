using E_Shop.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IOrder _order;

        public AdminController(IOrder order)
        {
            _order = order;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get Pending Order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPendingOrder()
        {
            var list = await _order.GetAll();
            return new JsonResult(list);
        }

        /// <summary>
        /// get Complete Order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCompleteOrder()
        {
            var list = await _order.GetCompleteOrder();
            return new JsonResult(list);
        }
    }
}
