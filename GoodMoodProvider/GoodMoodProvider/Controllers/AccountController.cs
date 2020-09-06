using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog.Core;
using Serilog;
using System.Xml;
using ContextLibrary.DataContexts;
using RepositoryLibrary.RepositoryInterface;
using WorkingLibrary.DataContexts.WorkingUnit;
using ModelsLibrary;
using RepositoryLibrary;
using GoodMoodProvider.DbInitializer.Interfaces;
using ModelsLibrary.ViewModels;
using UserService.Interfaces;
using UserService;

namespace GoodMoodProvider.Controllers
{
    public class AccountController : Controller
    {

        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncrypter _encrypter;

        public AccountController(DataContext context, IUnitOfWork unitOfWork)
        {
            _encrypter = new Encrypter();
            _context = context;
            _unitOfWork = unitOfWork;
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
            User newUser = new User()
                {
                  ID = new Guid(),
                  Password = _encrypter.EncryptString(model.Password),
                  Login = model.Login,
                  Email = model.Email
                };

            var role = _context.Role.FirstOrDefault(r => r.Name == "User");

            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.UserRoleRepository.AddAsync(new UserRole()
                {
                    ID = new Guid(),
                    UserID = newUser.ID,
                    RoleID = role.ID
                });

            await _unitOfWork.SaveDBAsync();
            Log.Logger.Information($"Info|{DateTime.Now}|New user {newUser.Login}|{newUser.ID}");

            if (User != null)
            {
                await Authenticate(newUser);
                return RedirectToAction("Index", "Home");
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
            User user = _context.User
                .Include(u => u.UserRoles)
                .FirstOrDefault(x => 
                   x.Login == model.Login && 
                   x.Password == _encrypter.EncryptString(model.Password));
          
            if(user!=null)
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

            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync((CookieAuthenticationDefaults.AuthenticationScheme));
            Log.Logger.Information($"Info|{DateTime.Now}|User {HttpContext.User.Identity.Name} has left application");
            return RedirectToAction("Login");
        }



        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser( Guid CurrentUserID)
        {
                await _unitOfWork.UserRepository.DeleteAsync(CurrentUserID);
            Log.Logger.Information($"Info|{DateTime.Now}|" +
                $"User {_context.User.FirstOrDefault(u => CurrentUserID == u.ID).Login} profile has been deleted|" +
                $"{CurrentUserID}");
            return RedirectToAction("Registration");
        }
    }
}