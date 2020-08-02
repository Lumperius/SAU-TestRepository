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

namespace GoodMoodProvider.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdminInitializer _adminInitializer;


        public HomeController(ILogger<HomeController> logger, AdminInitializer adminInitializer)
        {
            _adminInitializer = adminInitializer;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            Log.Information("Home was visited");
            await _adminInitializer.InitializeAsync();

            if (HttpContext.User.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "User")
             || HttpContext.User.HasClaim(ClaimsIdentity.DefaultRoleClaimType, "Admin"))
            { return View(); }
       else { return RedirectToAction( "Login" ,"Account"); }
            }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
        
        public IActionResult Show()
        {
            return View();
        }



    }
}
