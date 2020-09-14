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

        public IRepository<News> NewsRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Role> RoleRepository { get; set; }
        public IRepository<UserRole> UserRoleRepository { get; set; }


        public UnitOfWork(DataContext context, IRepository<News> newsRepository,
            IRepository<User> userRepository, IRepository<Role> roleRepository, IRepository<UserRole> userRoleRepository)
        {
            _context = context;
            NewsRepository = newsRepository;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserRoleRepository = userRoleRepository;
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
