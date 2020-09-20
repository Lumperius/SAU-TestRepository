using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLibrary.RepositoryInterface
{
    public interface IRepository<T> 
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task DeleteAsync(Guid id);
        public Task DeleteRangeAsync(IEnumerable<Guid> id);
        public Task AddAsync(T obj);
        public Task AddRangeAsync(IEnumerable<T> objs);
        public Task PutAsync(T obj);
        public Task ClearAsync();

    }
}
