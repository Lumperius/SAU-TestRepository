using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using ContextLibrary.DataContexts;
using ModelsLibrary;
using NewsUploader;
using Serilog.Core;
using ModelsLibrary.ViewModels;
using NewsUploader.Interfaces;
using RepositoryLibrary.RepositoryInterface;

namespace GoodMoodProvider.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsService _newsService;

        public NewsController(DataContext context, IUnitOfWork workingUnit, INewsService newsService)
        {
            _context = context;
            _unitOfWork = workingUnit;
            _newsService = newsService;
        }
       
              
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadNews()
        {
            await _newsService.LoadNewsInDb("https://news.tut.by/rss/all.rss");
            await _newsService.GetAllNewsBody();
            return RedirectToAction("NewsList");
        }


        [HttpGet]
        public IActionResult NewsList()
        {
            Log.Warning("NewsList was visited");
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
                Source = model.OriginSite,
                Author = model.Author,
                DatePosted = DateTime.Now
            };
            _context.News.Add(newNews);
            _context.SaveChanges();
            Log.Logger.Information($"Info|{DateTime.Now}|News {newNews.Article} were added|{newNews.ID}");
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
            var targetNews = _context.News
                .Where(N => N.ID == id)
                .FirstOrDefault();

            targetNews.Article = model.Article;
            targetNews.Body = model.Body;
            targetNews.Author = model.Author;
            targetNews.Source = model.OriginSite;

            _context.SaveChanges();
            Log.Logger.Information($"Info|{DateTime.Now}|News {targetNews.Article} were edited|{targetNews.ID}");
            return RedirectToAction("NewsList");
        }


        [HttpGet]
        public IActionResult EditNews(NewsViewModel model)
        {
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            _context.News.Remove(await _context.News.FirstOrDefaultAsync(n => n.ID == id));
            await _unitOfWork.SaveDBAsync();
          
            Log.Logger.Information($"Info|{DateTime.Now}|" +
                $"News {_context.News.FirstOrDefault(n => n.ID == id).Article} were deleted|" +
                $"{id}");
            return RedirectToAction("NewsList");
        }



        [HttpGet]
        public IActionResult DeleteNews()
        {
            return View();
        }


    }
}