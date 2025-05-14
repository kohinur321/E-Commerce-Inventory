using E_Shop.Services.Interface;
using E_Shop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrder _order;

        public OrderController(IOrder order)
        {
            _order = order;
        }
        /// <summary>
        /// get pending Order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _order.GetAll();
            return View(data);
        }
        /// <summary>
        /// get Product details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await _order.GetById(id);
            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> GetInit(int id)
        {
            var data = await _order.GetItemDetails(id);
            return new JsonResult(data);
        }

        [HttpPost]
        public async Task<IActionResult> Details(OrderViewModel model)
        {
            try
            {
                var status = await _order.ConfirmOrder(model);

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
    }
}
