using MeetHub.Domain.Entities;
using MeetHub.Domain.Interfaces;
using MeetHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeetHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MeetHubDbContext _context;

        public UserRepository(MeetHubDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmail(string email)
        {
           return await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByLogin(string login)
        {
           return await _context.Users.AsNoTracking().AnyAsync(u => u.Login == login);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<User?> GetByLoginOrEmail(string value)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == value);

            if (user == null)
                user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == value);

            return user;
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
