using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.RepositoryInterface
{
    public interface IUnitOfWork
    {
        public NewsRepository NewsRepository {get; set;}
        public UserRepository UserRepository { get; set; }
        public RoleRepository RoleRepository { get; set; }
        public UserRoleRepository UserRoleRepository { get; set; }

        public void SaveDB();
        public Task SaveDBAsync();

    }
}
