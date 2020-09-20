using ContextLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using RepositoryLibrary.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class UserRoleRepository : IRepository<UserRole>
    {
        private readonly DataContext _context;
        public UserRoleRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<UserRole> userRoles)
        {
            await _context.UserRoles.AddRangeAsync(userRoles);
            await _context.SaveChangesAsync();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (_context.UserRoles.Any(U => U.ID == id))
                _context.UserRoles.Remove(_context.UserRoles.FirstOrDefault(U => U.ID == id));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> id)
        {
            if (_context.UserRoles.Any(U => U.ID == id.FirstOrDefault(i => i == U.ID)))
                _context.UserRoles.RemoveRange(_context.UserRoles.Where(u => u.ID == id.FirstOrDefault(i => i == u.ID)));
            await _context.SaveChangesAsync();
        }

        public async Task<UserRole> GetByIdAsync(Guid id)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(n => n.ID == id);
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task PutAsync(UserRole userRole)
        {
            UserRole oldUserRole = await _context.UserRoles.FirstOrDefaultAsync(n => n.ID == userRole.ID);
            oldUserRole = userRole;
        }
    }
}
