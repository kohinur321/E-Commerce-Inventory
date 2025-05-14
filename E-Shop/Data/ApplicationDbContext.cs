using E_Shop.Models.Admin;
using E_Shop.Models.User;
using Microsoft.EntityFrameworkCore;
using E_Shop.Models;

namespace E_Shop.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {            
        }

        public DbSet<ProductModel> Product { get; set; }
        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<CartModel> Cart{ get; set; }
        public DbSet<SupplierModel> Suppliers { get; set; }
        public DbSet<StoreModel> Stores { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<StockTypeModel> StockTypes { get; set; }
        public DbSet<LedgerModel> Ledgers { get; set; }
        public DbSet<InventoryTypeModel> InventoryTypes { get; set; }
        public DbSet<PurchaseModel> Purchases { get; set; }
        public DbSet<PurchaseDetailModel> PurchaseDetails { get; set; }

    }
}
