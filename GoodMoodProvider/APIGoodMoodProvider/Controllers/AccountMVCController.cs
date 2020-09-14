using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog.Core;
using Serilog;
using System.Xml;
using ContextLibrary.DataContexts;
using RepositoryLibrary.RepositoryInterface;
using WorkingLibrary.DataContexts.WorkingUnit;
using RepositoryLibrary;
using APIGoodMoodProvider.Initializer;
using ModelsLibrary.Models;

namespace GoodMoodProvider.Controllers
{
    public class AccountMVCController : Controller
    {

        private readonly DataContext _context;
        private readonly IRepository<User> _userRepository;
        private readonly WorkingUnit _workingUnit;

        public AccountMVCController(DataContext context)
        {
            _context = context;
            _workingUnit = new WorkingUnit(_context);
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
        public async Task<IActionResult> Registration(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            User newUser = new User()
            {
                ID = new Guid(),
                Password = model.Password,
                Login = model.Login,
                Email = model.Email,
                Gender = model.Gender,
            };
            var role = _context.Role.FirstOrDefault(r => r.Name == "User");

            await _context.User.AddAsync(newUser);

            await _context.UserRoles.AddAsync(new UserRole()
            {
                ID = new Guid(),
                UserID = newUser.ID,
                RoleID = role.ID
            });

            await _workingUnit.SaveDBAsync();
            Log.Logger.Information($"Info|{DateTime.Now}|New user {newUser.Login}|{newUser.ID}");

            if (User != null)
            {
                await Authenticate(newUser);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Icorrect login or password");
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
            User user = _context.User
                .Include(u => u.UserRoles)
                .FirstOrDefault(x =>
               x.Login == model.Login &&
               x.Password == model.Password);

            if (User != null)
            {
                await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Icorrect login or password");
            Log.Logger.Information($"Info|{DateTime.Now}|User logged in {user.Login}|{user.ID}");

            return View();
        }




        private async Task<IActionResult> Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };

            claims.AddRange(user.UserRoles
                .Select(ur => new Claim(ClaimsIdentity.DefaultRoleClaimType, _context.Role
                .FirstOrDefault(r => r.ID == ur.RoleID).Name))
                  .ToList());

            var id = new ClaimsIdentity(claims,
                "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync((CookieAuthenticationDefaults.AuthenticationScheme));
            Log.Logger.Information($"Info|{DateTime.Now}|User {HttpContext.User.Identity.Name} has left");
            return RedirectToAction("Login");
        }
    }
}
