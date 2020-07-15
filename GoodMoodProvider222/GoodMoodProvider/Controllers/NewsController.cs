using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GoodMoodProvider.DataContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GoodMoodProvider.ViewsModels;
using GoodMoodProvider.Models;

namespace GoodMoodProvider.Controllers
{
    public class NewsController : Controller
    {
        public readonly DataContext _context;

        public NewsController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewsList(NewsViewModel model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContexts.DataContext>();

            return View();
        }
    }
}