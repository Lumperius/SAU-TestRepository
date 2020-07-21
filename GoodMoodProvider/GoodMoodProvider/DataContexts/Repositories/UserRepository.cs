using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodMoodProvider.DataContexts.Repositories.RepositoryInteface;
using GoodMoodProvider.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodMoodProvider.DataContexts.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<int> AddAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddRangeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            if(_context.User.Any(U => U.ID == id))
           _context.User.Remove( _context.User.FirstOrDefault( U => U.ID == id));
          

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(Guid ID)
        {
            throw new NotImplementedException();
        }
    }
}
