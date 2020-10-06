using ContextLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelsLibrary;
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace APIGoodMoodProvider.Initializer
{
    public class AdminInitializer : IAdminInitializer
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IEncrypter _encrypter;

        public AdminInitializer(DataContext context, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _config = config;
        }
        /// <summary>
        /// Create admin if not created already
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            var adminInfo = new User();
            _config.GetSection("AdminInfo").Bind(adminInfo);
            var test = adminInfo.Password;
            if (!await _context.Role.AnyAsync(R => R.Name == "Admin"))
            {
                await _unitOfWork.RoleRepository.AddAsync(new Role() { Name = "Admin", ID = new Guid() });
                await _unitOfWork.SaveDBAsync();
            }

            if (!await _context.Role.AnyAsync(R => R.Name == "User"))
            {
                await _unitOfWork.RoleRepository.AddAsync(new Role() { Name = "User", ID = new Guid() });
                await _unitOfWork.SaveDBAsync();
            }

            if (!await _context.User.AnyAsync(U => U.Login == adminInfo.Login))
            {
                await _unitOfWork.UserRepository.AddAsync(new User() { Login = adminInfo.Login, ID = new Guid(), Password = _encrypter.EncryptString(adminInfo.Password) });
                await _unitOfWork.SaveDBAsync();
            }

            if (!await _context.UserRoles.AnyAsync(UR => UR.UserID == _context.User
            .FirstOrDefault(U => U.Login == adminInfo.Login)
            .ID))
            {
                await _context.UserRoles.AddAsync(new UserRole()
                {
                    ID = new Guid(),
                    UserID = _context.User.FirstOrDefault(U => U.Login == adminInfo.Login).ID,
                    RoleID = _context.Role.FirstOrDefault(R => R.Name == "Admin").ID
                });
                await _context.UserRoles.AddAsync(new UserRole()
                {
                    ID = new Guid(),
                    UserID = _context.User.FirstOrDefault(U => U.Login == adminInfo.Login).ID,
                    RoleID = _context.Role.FirstOrDefault(R => R.Name == "User").ID
                });
                await _unitOfWork.SaveDBAsync();
            }
        }
    }
}
