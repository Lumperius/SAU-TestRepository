using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.DataContexts.Repositories.RepositoryInteface
{
    interface IRepository<T>
    {
        public Task<int> DeleteAsync(Guid ID);
        public Task<int> DeleteRangeAsync(Guid[] ID);
        public Task<int> AddAsync(T obj);
        public Task<int> AddRangeAsync(T[] objs);
    }
}
