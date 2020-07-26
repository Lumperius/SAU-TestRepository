using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodMoodProvider.Models;
using GoodMoodProvider.ViewsModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using GoodMoodProvider.DataContexts;
using GoodMoodProvider.DataContexts.Repositories.RepositoryInteface;
using GoodMoodProvider.DataContexts.Repositories;

namespace GoodMoodProvider.Controllers
{
    public class AccountController : Controller
    {

        private readonly DataContext _context;
        private readonly IRepository<User> _userRepository;

        public AccountController(DataContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(UserViewModel model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContexts.DataContext>();
            var options = optionsBuilder
                    .UseSqlServer(@"Server=DESKTOP-I8BJOOE;Database=GoodNewsDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;
            using (DataContexts.DataContext DbData = new DataContexts.DataContext((DbContextOptions<DataContexts.DataContext>)options))
            {
                User newUser = new User()
                {
                    ID = new Guid(),
                    Password = model.Password,
                    Login = model.Login,
                    Firstname = model.Firstname,
                    SecondName = model.SecondName,
                    BirthDay = model.BirthDay,
                    Gender = model.Gender,
                    IsOnline = true,
                };
            var role = _context.Role.FirstOrDefault(R => R.Name == "User");
            
            _context.UserRoles.Add(new UserRole()
                {
                    ID = new Guid(),
                    UserID = newUser.ID,
                    RoleID = role.ID
                });

            _context.User.Add(newUser);

                DbData.SaveChanges();

            }

            return View();
        }




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContexts.DataContext>();
            var options = optionsBuilder
                    .UseSqlServer(@"Server=DESKTOP-I8BJOOE;Database=GoodNewsGoodNewsDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;

                    User user = _context.User
                .Include(u => u.UserRoles)
                .ThenInclude(u => u.Role)
                .FirstOrDefault(x => 
               x.Login == model.Login && 
               x.Password == model.Password);
          
            if(User!=null)
            {
                await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Icorrect login or password");
            return RedirectToAction("Index", "Home");
        }




        private async Task<IActionResult> Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };


            claims.AddRange(user.UserRoles
                .Select(ur => new Claim(ClaimsIdentity.DefaultRoleClaimType, ur.Role.Name))
                .ToList());

            var id = new ClaimsIdentity(claims,
                "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            return RedirectToAction("Index", "Home");

        }


        private async Task<IActionResult> Logout(string userNickname)
        {
            await HttpContext.SignOutAsync((CookieAuthenticationDefaults.AuthenticationScheme));
            return RedirectToAction("Login");
        }




        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteUser( Guid CurrentUserID)
        {
                _userRepository.DeleteAsync(CurrentUserID);
            return RedirectToAction("Registration");
        }


    }
}