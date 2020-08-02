using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLibrary.RepositoryInterface
{
    public interface IRepository<T> 
    {
        public Task DeleteAsync(Guid ID);
        public Task DeleteRangeAsync(IEnumerable<Guid> ID);
        public Task AddAsync(T obj);
        public Task AddRangeAsync(IEnumerable<T> objs);
    }
}
