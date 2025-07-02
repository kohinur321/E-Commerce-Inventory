using System.Collections;
using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.Utilities;
using E_Shop.ViewModel;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class SupplierRepo : ISupplier
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SupplierRepo(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseStatus> Create(SupplierViewModel model)
        {
            var status = new ResponseStatus();
            if (IsExisting(model.SupplierName))
            {
                status.StatusCode = 1;
                status.Message = "Supplier Name is already exist!";
                return status;
            }
            try
            {
                SupplierModel supplier = new SupplierModel()
                {
                    SupplierId = model.SupplierId,
                    SupplierName = model.SupplierName,
                    Address = model.Address,
                    ContactNumber = model.ContactNumber,
                    Email = model.Email
                };

                _context.Suppliers.Add(supplier);

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
            catch (Exception ex)
            {
                status.StatusCode = 0;
                status.Message = "An error occurred during creation: " + ex.Message;
                return status;
            }
        }

        public async Task<ResponseStatus> Delete(int id)
        {
            var status = new ResponseStatus();
            var supplier = await _context.Suppliers.Where(x => x.SupplierId == id).FirstOrDefaultAsync();
            if (supplier == null)
            {
                status.StatusCode = 0;
                status.Message = "Deletion Failed";
                return status;
            }

            _context.Suppliers.Remove(supplier);
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

        public async Task<List<SupplierViewModel>> GetAll()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            var data = _mapper.Map<List<SupplierViewModel>>(suppliers);
            return data;
        }

        public Task<IEnumerable> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<SupplierModel>> GetAllSupplier()
        {
           return await _context.Suppliers.ToListAsync();
        }

        public async Task<SupplierViewModel> GetById(int id)
        {
            var supplier = await _context.Suppliers.Where(x => x.SupplierId == id).FirstOrDefaultAsync();
            var data = _mapper.Map<SupplierViewModel>(supplier);
            return data;
        }

        public async Task<ResponseStatus> Update(SupplierViewModel model)
        {
            var status = new ResponseStatus();

            if (IsExisting(model.SupplierName, model.SupplierId))
            {
                status.StatusCode = 0;
                status.Message = "Supplier Name already exists!";
                return status;
            }

            var existingSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == model.SupplierId);
            if (existingSupplier == null)
            {
                status.StatusCode = 0;
                status.Message = "Supplier not found";
                return status;
            }

            existingSupplier.SupplierId = model.SupplierId;
            existingSupplier.SupplierName = model.SupplierName;
            existingSupplier.Address = model.Address;
            existingSupplier.ContactNumber = model.ContactNumber;
            existingSupplier.Email = model.Email;

            _context.Suppliers.Update(existingSupplier);
            var result = await _context.SaveChangesAsync();

            if (result < 1)
            {
                status.StatusCode = 0;
                status.Message = "Updation Failed";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "Updated successfully";
            return status;
        }

        private bool IsExisting(string name, int id)
        {
            var count = _context.Suppliers.Where(x => x.SupplierName.ToLower() == name.ToLower() && x.SupplierId != id).Count();
            return count > 0;
        }

        private bool IsExisting(string name)
        {
            var count = _context.Suppliers.Where(x => x.SupplierName.ToLower() == name.ToLower()).Count();
            return count > 0;
        }
    }
}
