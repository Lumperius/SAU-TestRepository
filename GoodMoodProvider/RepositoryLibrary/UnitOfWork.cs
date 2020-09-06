using ContextLibrary.DataContexts;
using ModelsLibrary;
using RepositoryLibrary.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
       
        public NewsRepository NewsRepository { get; set; }
        public UserRepository UserRepository { get; set; }
        public RoleRepository RoleRepository { get; set; }
        public UserRoleRepository UserRoleRepository { get; set; }


        public UnitOfWork(DataContext context)
        {
            _context = context;
            NewsRepository = new NewsRepository(context);
            UserRepository = new UserRepository(context);
            RoleRepository = new RoleRepository(context);
            UserRoleRepository = new UserRoleRepository(context);
        }

        public void SaveDB()
        {
           _context.SaveChangesAsync();
        }

        public async Task SaveDBAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
