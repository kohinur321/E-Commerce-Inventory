using System.Security.Cryptography.Xml;
using E_Shop.Models.Admin;
using E_Shop.Utilities;
using E_Shop.ViewModels;

namespace E_Shop.Services.Interface
{
    public interface ITransfer
    {
        Task<List<TransferViewModel>> GetAll();

        Task<TransferViewModel> GetById(int id);

        Task<TransferViewModel> CreateMaster(TransferViewModel model);

        Task<TransferDetailViewModel> CreateDetail(TransferDetailViewModel detail);

        Task RemoveDetail(int id);

        Task<TransferViewModel> Approve(TransferViewModel model);

        Task DeleteAsync(int id);
    }
}