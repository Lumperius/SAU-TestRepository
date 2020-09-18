using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ModelsLibrary;
using RepositoryLibrary.RepositoryInterface;

namespace RepositoryLibrary
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }


        public async Task AddAsync(User user)
        {
            await _context.User.AddAsync(user);
        }


        public async Task AddRangeAsync(IEnumerable<User> users)
        {
            await _context.User.AddRangeAsync(users);
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            if(await _context.User.AnyAsync(U => U.ID == id))
           _context.User.Remove(await _context.User.FirstOrDefaultAsync( U => U.ID == id));          
        }


        public async Task DeleteRangeAsync(IEnumerable<Guid> id)
        {
            if (await _context.User.AnyAsync(U => U.ID ==  id.FirstOrDefault(i => i == U.ID)))
                _context.User.RemoveRange(_context.User.Where(u => u.ID == id.FirstOrDefault( i => i == u.ID)));
        }


        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task PutAsync(User user)
        {
            User oldUser = await _context.User.FirstOrDefaultAsync(n => n.ID == user.ID);
            oldUser = user;
        }
    }
}
