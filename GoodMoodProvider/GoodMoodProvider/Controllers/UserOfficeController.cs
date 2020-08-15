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

namespace GoodMoodProvider.Controllers
{
    public class UserOfficeController : Controller
    {


        public UserOfficeController( IAdminInitializer adminInitializer)
        {
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
