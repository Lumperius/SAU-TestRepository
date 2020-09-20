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
    public class RoleRepository : IRepository<Role>
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Role role)
        {
            await _context.Role.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Role> roles)
        {
            await _context.Role.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (_context.Role.Any(U => U.ID == id))
                _context.Role.Remove(_context.Role.FirstOrDefault(U => U.ID == id));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> id)
        {
            if (_context.Role.Any(U => U.ID == id.FirstOrDefault(i => i == U.ID)))
                _context.Role.RemoveRange(_context.Role.Where(u => u.ID == id.FirstOrDefault(i => i == u.ID)));
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await _context.Role.FirstOrDefaultAsync(n => n.ID == id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task PutAsync(Role role)
        {
            Role oldRole = await _context.Role.FirstOrDefaultAsync(r => r.ID == role.ID);
            oldRole = role;
        }
    }

}

