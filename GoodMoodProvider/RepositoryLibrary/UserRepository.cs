using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using Microsoft.AspNetCore.Mvc;
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


        public async Task<int> AddAsync(User user)
        {
            await _context.User.AddAsync(user);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(User[] users)
        {
            await _context.User.AddRangeAsync(users);

            return await _context.SaveChangesAsync();
        }

        public Task AddRangeAsync(IEnumerable<User> objs)
        {
            throw new NotImplementedException();
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if(_context.User.Any(U => U.ID == id))
           _context.User.Remove( _context.User.FirstOrDefault( U => U.ID == id));          

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(Guid[] id)
        {
            if (_context.User.Any(U => U.ID == id.FirstOrDefault(i => i == U.ID)))
                _context.User.RemoveRange(_context.User.Where(u => u.ID == id.FirstOrDefault( i => i == u.ID)));

            return await _context.SaveChangesAsync();
        }

        public Task DeleteRangeAsync(IEnumerable<Guid> id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(User obj)
        {
            throw new NotImplementedException();
        }

        Task IRepository<User>.AddAsync(User obj)
        {
            throw new NotImplementedException();
        }

        Task IRepository<User>.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
