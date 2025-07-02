using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class ProductTypeRepo : IProductType
    {
        private readonly ApplicationDbContext _context;

        public ProductTypeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(ProductTypeViewModel productTypeViewModel)
        {
            if (productTypeViewModel == null)
                return new BadRequestResult();

            var model = new ProductTypeModel
            {
                Name = productTypeViewModel.Name,
                IsActive = productTypeViewModel.IsActive
            };

            _context.ProductType.Add(model);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<List<ProductTypeViewModel>> GetAll()
        {
            return await _context.ProductType
                .Select(pt => new ProductTypeViewModel
                {
                    ProductTypeId = pt.ProductTypeId,
                    Name = pt.Name,
                    IsActive = pt.IsActive
                }).ToListAsync();
        }

        public async Task<IList<ProductTypeModel>> GetAllCategories()
        {
            return await _context.ProductType.ToListAsync();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productType = await _context.ProductType.FindAsync(id);
            if (productType == null)
                return new NotFoundResult();

            _context.ProductType.Remove(productType);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> Update(ProductTypeViewModel productTypeViewModel)
        {
            var productType = await _context.ProductType.FindAsync(productTypeViewModel.ProductTypeId);
            if (productType == null)
                return new NotFoundResult();

            productType.Name = productTypeViewModel.Name;
            productType.IsActive = productTypeViewModel.IsActive;

            _context.ProductType.Update(productType);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<ProductTypeViewModel> GetById(int id)
        {
            var productType = await _context.ProductType.FindAsync(id);
            if (productType == null) return null;

            return new ProductTypeViewModel
            {
                ProductTypeId = productType.ProductTypeId,
                Name = productType.Name,
                IsActive = productType.IsActive
            };
        }
    }
}
