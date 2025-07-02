using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModel;
using E_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Services.Repository
{
    public class TransferRepo : ITransfer
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransferRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all transfers (without details)
        public async Task<List<TransferViewModel>> GetAll()
        {
            var list = await (from t in _context.Transfers
                              select new TransferViewModel
                              {
                                  TransferId = t.TransferId,
                                  Description = t.Description,
                                  IsApprove = t.IsApprove,
                              }).ToListAsync();

            return list;
        }

        // Get transfer by Id including details
        public async Task<TransferViewModel> GetById(int id)
        {
            var transfer = await _context.Transfers
                                         .Include(t => t.TransferDetails)
                                         .FirstOrDefaultAsync(t => t.TransferId == id);

            if (transfer == null) return null;

            var details = transfer.TransferDetails.Select(d => new TransferDetailViewModel
            {
                TransferDetailId = d.TransferDetailId,
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                SourceStore = d.SourceStore,
                DistinationStore = d.DistinationStore
            }).ToList();

            return new TransferViewModel
            {
                TransferId = transfer.TransferId,
                Description = transfer.Description,
                IsApprove = transfer.IsApprove,
                TransferDetails = details
            };
        }

        // Create or update Transfer Master record
        public async Task<TransferViewModel> CreateMaster(TransferViewModel model)
        {
            var transfer = await _context.Transfers.FirstOrDefaultAsync(t => t.TransferId == model.TransferId);

            if (transfer == null)
            {
                transfer = new TransferModel
                {
                    Description = model.Description,
                    IsApprove = model.IsApprove
                };

                _context.Transfers.Add(transfer);
                await _context.SaveChangesAsync();
                model.TransferId = transfer.TransferId;
            }
            else
            {
                transfer.Description = model.Description;
                transfer.IsApprove = model.IsApprove;
                await _context.SaveChangesAsync();
            }

            return model;
        }

        // Create Transfer Detail record
        public async Task<TransferDetailViewModel> CreateDetail(TransferDetailViewModel detail)
        {
            var detailModel = new TransferDetailModel
            {
                TransferId = detail.TransferId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                SourceStore = detail.SourceStore,
                DistinationStore = detail.DistinationStore
            };

            _context.TransferDetails.Add(detailModel);
            await _context.SaveChangesAsync();

            detail.TransferDetailId = detailModel.TransferDetailId;
            return detail;
        }

        // Remove Transfer Detail by Id
        public async Task RemoveDetail(int id)
        {
            var detail = await _context.TransferDetails.FirstOrDefaultAsync(d => d.TransferDetailId == id);
            if (detail != null)
            {
                _context.TransferDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }

        // Approve transfer (example implementation)
        public async Task<TransferViewModel> Approve(TransferViewModel model)
        {
            var transfer = await _context.Transfers.FirstOrDefaultAsync(t => t.TransferId == model.TransferId);
            if (transfer != null)
            {
                transfer.IsApprove = true;
                await _context.SaveChangesAsync();
                model.IsApprove = true;
            }
            return model;
        }

        // Delete Transfer including its details
        public async Task DeleteAsync(int id)
        {
            var transfer = await _context.Transfers
                                         .Include(t => t.TransferDetails)
                                         .FirstOrDefaultAsync(t => t.TransferId == id);

            if (transfer != null)
            {
                _context.TransferDetails.RemoveRange(transfer.TransferDetails);
                _context.Transfers.Remove(transfer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
