using RepositoryLibrary.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary;

namespace RepositoryLibrary
{
    class TokenRepository : IRepository<Token>
    {
        public Task AddAsync(Token obj)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Token> objs)
        {
            throw new NotImplementedException();
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<Guid> ID)
        {
            throw new NotImplementedException();
        }
    }
}
