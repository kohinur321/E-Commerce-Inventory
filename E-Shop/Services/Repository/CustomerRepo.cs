using AutoMapper;
using E_Shop.Data;
using E_Shop.Migrations;
using E_Shop.Models.Admin;
using E_Shop.Models.User;
using E_Shop.Services.Interface;
using E_Shop.Utilities;
using E_Shop.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_Shop.Services.Repository
{
    public class CustomerRepo:ICustomer
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CustomerRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseStatus> Save(CustomerViewModel model)
        {
            var status = new ResponseStatus();
            var data = _mapper.Map<CustomerModel>(model);
            data.CustomerId = 0;
            _context.Customer.Add(data);
            var result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Creation Failed";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "Created successfully";
            status.Customer = data;
            if (data.CustomerId != 0)
            {
                OrderModel order = new OrderModel()
                {
                    CustomerId = data.CustomerId,
                    OrderDate = DateTime.Now,
                };
                _context.Order.Add(order);
                await _context.SaveChangesAsync();
            }
            var existdata = await _context.Order.Where(x=>x.isPending == false).FirstOrDefaultAsync();
            var cartdata = await _context.Cart.Where(x=>x.OrderId == 0).ToListAsync();
            foreach (var cart in cartdata)
            {
                cart.OrderId = existdata.OrderId;
            }
            existdata.isPending = true;
            _context.Order.Update(existdata);
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
