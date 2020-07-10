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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewsList(NewsViewModel model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContexts.DataContext>();

            var options = optionsBuilder
           .UseSqlServer(@"Server=DESKTOP-I8BJOOE;Database=GoodNewsDB;Trusted_Connection=True;MultipleActiveResultSets=true")
           .Options;

            using (DataContext DbData = new DataContext(options))
            {
                foreach(News item in DbData.News)
                {

                }
            }
            return View();
        }
    }
}