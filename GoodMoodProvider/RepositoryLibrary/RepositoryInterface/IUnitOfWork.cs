using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.RepositoryInterface
{
    public interface IUnitOfWork
    {
        public IRepository<News> NewsRepository {get; set;}
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Role> RoleRepository { get; set; }
        public IRepository<UserRole> UserRoleRepository { get; set; }

        public void SaveDB();
        public Task SaveDBAsync();

    }
}
