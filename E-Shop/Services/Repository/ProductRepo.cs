using System.Collections;
using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.Utilities;
using E_Shop.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class ProductRepo : IProduct
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepo(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseStatus> Create(ProductViewModel model)
        {
            var status = new ResponseStatus();
            if (isExising(model.Name))
            {
                status.StatusCode = 1;
                status.Message = "Product Name is already exist!";
                return status;
            }
            try
            {
                ProductModel product = new ProductModel()
                {
                    ProductId = model.ProductId,
                    Name = model.Name,
                    Price = model.Price,
                    Unit = model.Unit,
                    Image = model.Image,
                    ImageUrl = model.ImageUrl,
                    ImageName = model.ImageName,
                    Description = model.Description,
                    isAvailable = model.isAvailable
                };

                if (product.Image != null)
                {
                    string filename = UploadImage(model);
                    product.ImageUrl = filename;
                }
                _context.Product.Add(product);

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
            var productId = await _context.Product.Where(x=>x.ProductId == id).FirstOrDefaultAsync();
            if (productId == null)
            {
                status.StatusCode = 0;
                status.Message = "Deletion Failed";
                return status;
            }
            _context.Product.Remove(productId);
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

        public async Task<List<ProductViewModel>> GetAll()
        {
            var products = await _context.Product.ToListAsync();
            var data = _mapper.Map<List<ProductViewModel>>(products);
            return data;
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            var productId = await _context.Product.Where(x=>x.ProductId== id).FirstOrDefaultAsync();
            var data = _mapper.Map<ProductViewModel>(productId);
            return data;
        }

        public async Task<ResponseStatus> Update(ProductViewModel model)
        {
            var status = new ResponseStatus();

            if (isExising(model.Name, model.ProductId))
            {
                status.StatusCode = 0;
                status.Message = "Product Name already exists!";
                return status;
            }
            var existingProduct = await _context.Product.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
            if (existingProduct == null)
            {
                status.StatusCode = 0;
                status.Message = "Product not found";
                return status;
            }
            
            existingProduct.ProductId = model.ProductId;
            existingProduct.Name = model.Name;
            existingProduct.Unit = model.Unit;
            existingProduct.Price = model.Price;
            existingProduct.Description = model.Description;
            existingProduct.Image = model.Image;
            existingProduct.ImageName = model.ImageName;
            existingProduct.ImageUrl = model.ImageUrl;
            existingProduct.isAvailable = model.isAvailable;

            if (existingProduct.Image != null)
            {
                string filename = UploadImage(model);
                existingProduct.ImageUrl = filename;
            }
            _context.Product.Update(existingProduct);
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

        private string UploadImage(ProductViewModel item)
        {
            string filename = "";
            if (item.Image != null)
            {
                string UploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + item.Image.FileName;
                string filePath = Path.Combine(UploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    item.Image.CopyTo(fileStream);

                }
            }
            return filename;
        }

        private bool isExising(string name, int id)
        {
            var ct = _context.Product.Where(x => x.Name.ToLower() == name.ToLower() && x.ProductId != id).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }
        private bool isExising(string name)
        {
            var ct = _context.Product.Where(x => x.Name.ToLower() == name.ToLower()).Count();
            if (ct > 0)
                return true;
            else
                return false;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }
    }
}
