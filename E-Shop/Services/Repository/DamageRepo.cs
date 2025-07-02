using AutoMapper;
using E_Shop.Data;
using E_Shop.Models.Admin;
using E_Shop.Services.Interface;
using E_Shop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Services.Repository
{
    public class DamageRepo : IDamage
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DamageRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DamageViewModel>> GetAllDamagesAsync()
        {
            var damages = await _context.Damages
                .Include(d => d.DamageDetails)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DamageViewModel>>(damages);
        }

        public async Task<DamageViewModel> GetDamageByIdAsync(int id)
        {
            var damage = await _context.Damages
                .Include(d => d.DamageDetails)
                .FirstOrDefaultAsync(d => d.DamageId == id);

            return damage == null ? null : _mapper.Map<DamageViewModel>(damage);
        }

        public async Task<int> CreateDamageAsync(DamageViewModel damageViewModel)
        {
            var damage = _mapper.Map<DamageModel>(damageViewModel);
            _context.Damages.Add(damage);
            await _context.SaveChangesAsync();
            return damage.DamageId;
        }

        public async Task<bool> UpdateDamageAsync(DamageViewModel damageViewModel)
        {
            var existingDamage = await _context.Damages
                .Include(d => d.DamageDetails)
                .FirstOrDefaultAsync(d => d.DamageId == damageViewModel.DamageId);

            if (existingDamage == null)
                return false;

            _mapper.Map(damageViewModel, existingDamage);
            _context.Damages.Update(existingDamage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDamageAsync(int id)
        {
            var damage = await _context.Damages.FindAsync(id);
            if (damage == null)
                return false;

            _context.Damages.Remove(damage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveDamageAsync(int id)
        {
            var damage = await _context.Damages.FindAsync(id);
            if (damage == null)
                return false;

            damage.IsApprove = true;
            _context.Damages.Update(damage);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
