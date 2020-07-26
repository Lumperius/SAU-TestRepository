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
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.AspNetCore.Authorization;
using GoodMoodProvider.DataContexts.WorkingUnit;

namespace GoodMoodProvider.Controllers
{
    public class NewsController : Controller
    {
        public readonly DataContext _context;
        public readonly WorkingUnit _workingUnit;

        public NewsController(DataContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContexts.DataContext>();
            var options = optionsBuilder
                    .UseSqlServer(@"Server=DESKTOP-I8BJOOE;Database=GoodNewsGoodNewsData;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;
            _context = new DataContext((DbContextOptions<DataContext>)options);
            _workingUnit = new WorkingUnit(_context);
        }
       
              
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
 //       [Authorize(Roles = "Admin, User")]
        public IActionResult NewsList()
        {
            return View(_context.News);
        }


        [HttpPost]
  //      [Authorize(Roles = "Admin")]
        public IActionResult AddNews(NewsViewModel model)
        {

            var newNews = new News
            {
                ID = new Guid(),
                Article = model.Article,
                Body = model.Body,
                SourceSite = model.OriginSite,
                Author = model.Author,
                DatePosted = DateTime.Now
            };
            _context.News.Add(newNews);
            _context.SaveChanges();
            return View();
        }

        [HttpGet]
   //     [Authorize(Roles = "Admin")]
        public IActionResult AddNews()
        {
            return View();
        }


        [HttpPost]
   //     [Authorize(Roles = "Admin")]
        public IActionResult EditNews(NewsViewModel model, Guid id)
        {
            var TargetNews = _context.News
                .Where(N => N.ID == id)
                .FirstOrDefault();

            TargetNews.Article = model.Article;
            TargetNews.Body = model.Body;
            TargetNews.Author = model.Author;
            TargetNews.SourceSite = model.OriginSite;

            _context.SaveChanges();

            return RedirectToAction("News/NewsList");
        }


        [HttpGet]
     //   [Authorize(Roles = "Admin")]
        public IActionResult EditNews()
        {
            return View();
        }




        [HttpPost]
     //   [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            _context.News.Remove(await _context.News.FirstOrDefaultAsync(n => n.ID == id));
            await _workingUnit.SaveDBAsync();

            return RedirectToAction("News/NewsList");
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteNews()
        {
            return View();
        }


    }
}