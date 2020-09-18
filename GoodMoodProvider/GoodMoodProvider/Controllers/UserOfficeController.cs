using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoodMoodProvider.DbInitializer;
using System.Security.Claims;
using Serilog;
using ModelsLibrary;
using GoodMoodProvider.DbInitializer.Interfaces;
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using UserService.Interfaces;
using ModelsLibrary.Requests;
using ContextLibrary.DataContexts;

namespace GoodMoodProvider.Controllers
{
    public class UserOfficeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserHandler _userHandler;
        private readonly DataContext _context;

        public UserOfficeController( IAdminInitializer adminInitializer, IUnitOfWork unitOfWork, IUserHandler userHandler,
            DataContext context)
        {
            _unitOfWork = unitOfWork;
            _userHandler = userHandler;
            _context = context;
        }
        public IActionResult Index(User currentUser)
        {
            return View(currentUser);
        }

        [HttpGet]
        public IActionResult EditUserInfo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditUserInfo(User currentUser)
        {
            return View();
        }


    }
}
