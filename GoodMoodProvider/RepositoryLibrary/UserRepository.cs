using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using ContextLibrary.Interfaces;
using ModelsLibrary;
using RepositoryLibrary.RepositoryInterface;
using WorkingLibrary.DataContexts.WorkingUnit;

namespace RepositoryLibrary
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataContext _context;
        private readonly IWorkingUnit _workingUnit;

        public UserRepository(DataContext context, IWorkingUnit workingUnit)
        {
            _context = context;
            _workingUnit = workingUnit;
        }


        public async Task AddAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _workingUnit.SaveDBAsync();
        }

        public async Task AddRangeAsync(IEnumerable<User> users)
        {
            await _context.User.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if(_context.User.Any(U => U.ID == id))
           _context.User.Remove( _context.User.FirstOrDefault( U => U.ID == id));          
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> id)
        {
            if (_context.User.Any(U => U.ID == id.FirstOrDefault(i => i == U.ID)))
                _context.User.RemoveRange(_context.User.Where(u => u.ID == id.FirstOrDefault( i => i == u.ID)));
             await _context.SaveChangesAsync();
        }
    }
}
