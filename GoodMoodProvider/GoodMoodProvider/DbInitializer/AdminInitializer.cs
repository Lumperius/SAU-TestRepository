using ContextLibrary.DataContexts;
using ContextLibrary.Interfaces;
using GoodMoodProvider.DbInitializer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService;
using UserService.Interfaces;
using WorkingLibrary.DataContexts.WorkingUnit;

namespace GoodMoodProvider.DbInitializer
{
    public class AdminInitializer : IAdminInitializer
    {
        private readonly DataContext _context;
        private readonly IWorkingUnit _workingUnit;
        private readonly IEncrypter _encrypter;

        public AdminInitializer(DataContext context, IEncrypter encrypter, IWorkingUnit workingUnit)
        {
            _context = context;
            _workingUnit = workingUnit;
            _encrypter = encrypter;
        }

        public async Task InitializeAsync()
        {
            if(!await _context.Role.AnyAsync(R => R.Name == "Admin"))
            {
                await _context.Role.AddAsync(new Role() { Name = "Admin", ID = new Guid() });
                await _context.Role.ToListAsync();
                await _workingUnit.SaveDBAsync();
            }

            if (!await _context.Role.AnyAsync(R => R.Name == "User"))
            {
                await _context.Role.AddAsync(new Role() { Name = "User", ID = new Guid() });
                await _context.Role.ToListAsync();
                await _workingUnit.SaveDBAsync();
            }


            if (!await _context.User.AnyAsync(U => U.Login == "CEO"))
            {
                await _context.User.AddAsync(new User()
                { Login = "CEO", ID = new Guid(), Password = _encrypter.EncryptString("qwerty"), Email = "Admin@cool.wow"});
                await _context.User.ToListAsync();
                await _workingUnit.SaveDBAsync();
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
                await _workingUnit.SaveDBAsync();

            }

        }
    }
}
