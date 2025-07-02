using E_Shop.Models.Admin;
using E_Shop.ViewModels;
using E_Shop.Services.Interface;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_Shop.Data;

namespace E_Shop.Repository
{
    public class DamageDetailsRepo : IDamageDetails
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DamageDetailsRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DamageDetailViewModel>> GetAllAsync()
        {
            return await _context.DamageDetails
                .Include(d => d.Product)
                .Include(d => d.Store)
                .ProjectTo<DamageDetailViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<DamageDetailViewModel> GetByIdAsync(int id)
        {
            var damageDetail = await _context.DamageDetails
                .Include(d => d.Product)
                .Include(d => d.Store)
                .FirstOrDefaultAsync(d => d.DamageDetailId == id);

            return _mapper.Map<DamageDetailViewModel>(damageDetail);
        }

        public async Task AddAsync(DamageDetailViewModel model)
        {
            var entity = _mapper.Map<DamageDetailModel>(model);
            _context.DamageDetails.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DamageDetailViewModel model)
        {
            var entity = await _context.DamageDetails.FindAsync(model.DamageDetailId);
            if (entity != null)
            {
                _mapper.Map(model, entity);
                _context.DamageDetails.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DamageDetails.FindAsync(id);
            if (entity != null)
            {
                _context.DamageDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
