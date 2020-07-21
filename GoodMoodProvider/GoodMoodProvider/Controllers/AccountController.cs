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

namespace GoodMoodProvider.Controllers
{
    public class AccountController : Controller
    {

        private readonly DataContext _context;
        private readonly IRepository<User> _userRepository;

        public IActionResult Index()
        {
            return View();
        }

        /// /////////////////////////////////////////////////////////////

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
                User NewUser = new User();
                NewUser.ID = new Guid();
                NewUser.Password = model.Password;
                NewUser.Login = model.Login;
                NewUser.Firstname = model.Firstname;
                NewUser.SecondName = model.SecondName;
                NewUser.BirthDay = model.BirthDay;
                NewUser.Gender = model.Gender;
                NewUser.IsOnline = true;

                DbData.User.Add(NewUser);

                DbData.SaveChanges();

            }

            return View();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////

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
            using (DataContexts.DataContext DbData = new DataContexts.DataContext((DbContextOptions<DataContexts.DataContext>)options))
            {
                    User user = DbData.User.FirstOrDefault(x => 
                    x.Login == model.Login && 
                    x.Password == model.Password);
            }
            if(User!=null)
            {
                await Authenticate(model.Login);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Icorrect login or password");
            return View();
        }

        /// /////////////////////////////////////////////////////////////

        private async Task Authenticate(string userNickname)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userNickname)
            };

            var id = new ClaimsIdentity(claims,
                "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

/// /////////////////////////////////////////////////////////////

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