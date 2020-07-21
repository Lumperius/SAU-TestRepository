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

namespace GoodMoodProvider.Controllers
{
    public class NewsController : Controller
    {
        public readonly DataContext _context;

        public NewsController(DataContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContexts.DataContext>();
            var options = optionsBuilder
                    .UseSqlServer(@"Server=DESKTOP-I8BJOOE;Database=GoodNewsGoodNewsData;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;
            _context = new DataContext((DbContextOptions<DataContext>)options);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewsList()
        {
            return View(_context.News);
        }


        [HttpPost]
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
        public IActionResult AddNews()
        {
            return View();
        }


        [HttpPost]
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
        public IActionResult EditNews(Guid id)
        {
            var TargetNews = _context.News
    .Where(N => N.ID == id)
    .FirstOrDefault();
            NewsViewModel EditNews = new NewsViewModel();
            EditNews.Article = TargetNews.Article;
            EditNews.Body = TargetNews.Body;
            EditNews.Author = TargetNews.Author;
            EditNews.OriginSite = TargetNews.SourceSite;
            return View(EditNews);
        }


    }
}