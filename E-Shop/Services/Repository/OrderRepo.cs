using AutoMapper;
using E_Shop.Data;
using E_Shop.Migrations;
using E_Shop.Models.User;
using E_Shop.Services.Interface;
using E_Shop.Utilities;
using E_Shop.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class OrderRepo:IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseStatus> ConfirmOrder(OrderViewModel model)
        {
            var status = new ResponseStatus();
            var existingData = await _context.Order.Where(x => x.OrderId == model.OrderId && x.isComplete == false).FirstOrDefaultAsync();
            if(existingData != null)
            {
                existingData.isComplete= true;
            }
            _context.Order.Update(existingData);
            var result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Confirm Order Failed";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "Order successfully";
            return status;
        }

        public async Task<List<OrderViewModel>> GetAll()
        {
            var pendingdata = await _context.Order.Where(x=>x.isComplete == false).Include(x=>x.Customer).ToListAsync();
            var data = _mapper.Map<List<OrderViewModel>>(pendingdata);
            return data;
        }

        public async Task<OrderViewModel> GetById(int id)
        {
            var productId = await _context.Order.Where(x => x.OrderId == id).Include(x=>x.Customer).FirstOrDefaultAsync();
            var data = _mapper.Map<OrderViewModel>(productId);
            return data;
        }

        public async Task<List<CartViewModel>> GetItemDetails(int id)
        {
            var cartData = await _context.Cart.Where(x=>x.OrderId == id).Include(x=>x.Product).ToListAsync();
            var data =  _mapper.Map<List<CartViewModel>>(cartData);
            return data;
        }

        public async Task<List<OrderViewModel>> GetCompleteOrder()
        {
            var pendingdata = await _context.Order.Where(x => x.isComplete == true).Include(x => x.Customer).ToListAsync();
            var data = _mapper.Map<List<OrderViewModel>>(pendingdata);
            return data;
        }
    }
}
