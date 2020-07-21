﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoodMoodProvider.Models;
using GoodMoodProvider.DbInitializer;

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
            await _adminInitializer.InitializeAsync();
            return View();
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
