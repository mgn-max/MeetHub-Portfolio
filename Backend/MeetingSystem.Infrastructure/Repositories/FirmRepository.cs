using MeetHub.Domain.Entities;
using MeetHub.Domain.Interfaces;
using MeetHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace MeetHub.Infrastructure.Repositories
{
    public class FirmRepository : IFirmRepository
    {
        private readonly MeetHubDbContext _context;

        public FirmRepository(MeetHubDbContext context)
        {
            _context = context;
        }

        public async Task Add(Firm firm)
        {
            await _context.Firms.AddAsync(firm);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByCnpj(string cnpj)
        {
            return await _context.Firms.AsNoTracking().AnyAsync(f => f.Cnpj == cnpj);
        }

        public async Task<bool> ExistsByCnpjExceptId(string cnpj, Guid id)
        {
            return await _context.Firms.AsNoTracking().AnyAsync(f => f.Cnpj == cnpj && f.Id != id);
        }

        public async Task<Firm?> GetById(Guid id)
        {
          return await _context.Firms.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task Update(Firm firm)
        {
            _context.Firms.Update(firm);
            await _context.SaveChangesAsync();
        }
    }
}
