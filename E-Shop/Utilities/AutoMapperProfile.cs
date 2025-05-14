using AutoMapper;
using E_Shop.Models;
using E_Shop.Models.Admin;
using E_Shop.Models.User;
using E_Shop.ViewModel;
using E_Shop.ViewModels;


namespace E_Shop.Utilities
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductModel, ProductViewModel>().ReverseMap();
            CreateMap<CartModel, CartViewModel>().ReverseMap();
            CreateMap<CustomerModel, CustomerViewModel>().ReverseMap();
            CreateMap<OrderModel, OrderViewModel>().ReverseMap();
            CreateMap<StoreModel, StoreViewModel>().ReverseMap();
            CreateMap<SupplierModel, SupplierViewModel>().ReverseMap();
            CreateMap<UserModel, UserViewModel>().ReverseMap();
            CreateMap<RoleModel, RoleViewModel>().ReverseMap();
            CreateMap<UserRoleModel, UserRoleViewModel>().ReverseMap();
            CreateMap<StockModel, StockViewModel>().ReverseMap();
            CreateMap<StockTypeModel, StockTypeViewModel>().ReverseMap();
            CreateMap<InventoryTypeModel, InventoryTypeViewModel>().ReverseMap();
            CreateMap<LedgerModel, LedgerViewModel>().ReverseMap();
            CreateMap<PurchaseModel, PurchaseViewModel>().ReverseMap();
            CreateMap<PurchaseDetailModel, PurchaseDetailViewModel>().ReverseMap();
        }
    }
}
