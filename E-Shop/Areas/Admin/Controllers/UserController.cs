using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AutoMapper;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly IUser user;
        private readonly IMapper mapper;
        public UserController(IUser user, IMapper mapper)
        {
            this.user = user;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var list = await user.GetAll();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create()
        {
            UserViewModel userViewModel = new UserViewModel();
            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var data = await user.Create(userViewModel);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var allusers = await user.GetAll();
            var userViewModel = allusers.FirstOrDefault(s => s.UserId == id);

            if (userViewModel == null)
            {
                return NotFound();
            }
            return View(userViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await user.Delete(id);

            if (result is OkResult)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await user.GetById(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var updateuser = await user.Update(userViewModel);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
    }
}

