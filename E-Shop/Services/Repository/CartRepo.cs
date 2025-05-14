using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.User;
using E_Shop.Services.Interface;
using E_Shop.Utilities;
using E_Shop.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class CartRepo:ICart
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseStatus> Create(CartViewModel model)
        {
            var status = new ResponseStatus();

            var data = _mapper.Map<CartModel>(model); 
            var existingData = await _context.Cart
                .Where(x => x.ProductId == data.ProductId && x.OrderId == 0)
                .FirstOrDefaultAsync();

            if (existingData != null)
            {
                existingData.Quantity += data.Quantity;
                existingData.TotalPrice += data.TotalPrice;
                _context.Cart.Update(existingData);
            }
            else
            {
                _context.Cart.Add(data);
            }

            var result = await _context.SaveChangesAsync();

            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Creation Failed";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "Created successfully";
            return status;
        }

        public async Task<ResponseStatus> Delete(int id)
        {
            var status = new ResponseStatus();
            var cartId = await _context.Cart.Where(x => x.CartId == id).FirstOrDefaultAsync();
            if (cartId == null)
            {
                status.StatusCode = 0;
                status.Message = "Deletion Failed";
                return status;
            }
            _context.Cart.Remove(cartId);
            var result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Deletion Failed";
                return status;
            }
            status.StatusCode = 1;
            status.Message = "Delete successfully";
            return status;
        }

        public async Task<List<CartViewModel>> GetAll()
        {
            var products = await _context.Cart.Where(x=> x.OrderId == 0).Include(x=>x.Product).ToListAsync();
            var data = _mapper.Map<List<CartViewModel>>(products);
            return data;
        }
    }
}
