using ContextLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGoodMoodProvider.Initializer
{
    public class AdminInitializer : IAdminInitializer
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AdminInitializer(DataContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task InitializeAsync()
        {
            if (!await _context.Role.AnyAsync(R => R.Name == "Admin"))
            {
                await _context.Role.AddAsync(new Role() { Name = "Admin", ID = new Guid() });
                await _context.Role.ToListAsync();
                await _unitOfWork.SaveDBAsync();
            }

            if (!await _context.Role.AnyAsync(R => R.Name == "User"))
            {
                await _context.Role.AddAsync(new Role() { Name = "User", ID = new Guid() });
                await _context.Role.ToListAsync();
                await _unitOfWork.SaveDBAsync();
            }

            if (!await _context.User.AnyAsync(U => U.Login == "CEO"))
            {
                await _context.User.AddAsync(new User() { Login = "CEO", ID = new Guid(), Password = "qwerty"});
                await _context.User.ToListAsync();
                await _unitOfWork.SaveDBAsync();
            }

            if (!await _context.UserRoles.AnyAsync(UR => UR.UserID == _context.User
            .FirstOrDefault(U => U.Login == "CEO")
            .ID))
            {
                await _context.UserRoles.AddAsync(new UserRole()
                {
                    ID = new Guid(),
                    UserID = _context.User.FirstOrDefault(U => U.Login == "CEO").ID,
                    RoleID = _context.Role.FirstOrDefault(R => R.Name == "Admin").ID
                });
                await _context.UserRoles.AddAsync(new UserRole()
                {
                    ID = new Guid(),
                    UserID = _context.User.FirstOrDefault(U => U.Login == "CEO").ID,
                    RoleID = _context.Role.FirstOrDefault(R => R.Name == "User").ID
                });
                await _unitOfWork.SaveDBAsync();
            }
        }
    }
}
