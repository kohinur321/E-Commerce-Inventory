using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Repositories.Implementations;

//using E_Shop.Repositories.Admin;
using E_Shop.Repository;
using E_Shop.Service.Interface;
using E_Shop.Service.Repository;
using E_Shop.Services.Interface;
using E_Shop.Services.Repository;
using E_Shop.Utilities;
using Microsoft.EntityFrameworkCore;
using VehicleWorkShop.Service.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EShop")));

builder.Services.AddTransient<IProduct, ProductRepo>();
builder.Services.AddTransient<IProductType, ProductTypeRepo>();
builder.Services.AddTransient<ICart, CartRepo>();
builder.Services.AddTransient<ICustomer, CustomerRepo>();
builder.Services.AddTransient<IOrder, OrderRepo>();
builder.Services.AddTransient<IStore, StoreRepo>();
builder.Services.AddTransient<ISupplier, SupplierRepo>();
builder.Services.AddTransient<IUser, UserRepo>();
builder.Services.AddTransient<IRole, RoleRepo>();
builder.Services.AddTransient<IUserRole, UserRoleRepo>();
builder.Services.AddTransient<IStock, StockRepo>();
builder.Services.AddTransient<IStockType, StockTypeRepo>();
builder.Services.AddTransient<ITransaction, TransactionTypeRepo>();
//builder.Services.AddTransient<ILedger, LedgerRepo>();
builder.Services.AddTransient<IInventoryType, InventoryTypeRopo>();
builder.Services.AddTransient<IPurchase, PurchaseRepo>();
builder.Services.AddTransient<ITransfer, TransferRepo>();
builder.Services.AddTransient<ISale, SaleRepo>();
//builder.Services.AddTransient<ISaleDetails, SaleDetailsRepo>();
builder.Services.AddTransient<IDamage, DamageRepo>();
builder.Services.AddTransient<IDamageDetails, DamageDetailsRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
   // name: "default",
  // pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
name: "default",
    pattern: "{Area=User}/{controller=Home}/{action=Index}/{id?}"
);
app.Run();
