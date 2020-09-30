using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqsLibrary.Queries;
using CqsLibrary.Queries.NewsQueries;
using Hangfire;
using Hangfire.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using ModelsLibrary.Responces;
using NewsUploader.Interfaces;
using RepositoryLibrary.RepositoryInterface;

namespace APIGoodMoodProvider.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsService _newsService;
        public NewsController(IUnitOfWork unitOfWork, INewsService newsService, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _newsService = newsService;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int count = 50)
        {
            List<GetNewsResponse> responseList = new List<GetNewsResponse>();
            var query = new GetNewsAll();
            foreach ( News news in await _mediator.Send(query))
            {
                responseList.Add(new GetNewsResponse
                {
                    Id = news.ID,
                    Article = news.Article,
                    PlainText = news.PlainText,
                    Source = news.Source,
                    Rating = news.FinalRating
                });
                if(responseList.Count > 50 ) { break;}
            }
            return Ok(responseList);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _unitOfWork.NewsRepository.GetAllAsync());
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("SwitchHangFire")]
        public async Task<IActionResult> SwitchHangFire()
        {
            RecurringJobManager jobManager = new RecurringJobManager();

            RecurringJob.AddOrUpdate(
                "TutBy",
                () => _newsService.LoadNewsInDb("https://news.tut.by/rss/all.rss"),
                 Cron.Hourly);
            RecurringJob.AddOrUpdate(
                "Onliner",
                () => _newsService.LoadNewsInDb("http://Onliner.by/feed"),
                 Cron.Hourly);
            RecurringJob.AddOrUpdate(
                "S13",
                () => _newsService.LoadNewsInDb("https://S13.ru/rss"),
                 Cron.Hourly);

            RecurringJob.AddOrUpdate(
                "BodyParser",
                () => _newsService.GetAllNewsBody(),
                 Cron.Hourly);

            RecurringJob.AddOrUpdate(
                "NewsRater",
                () => _newsService.RateNewsInDb(),
                "*/ 30 * ***");

            //var news = await _unitOfWork.NewsRepository.GetAllAsync();

            //string idHolder = null;
            //foreach(News newsItem in news)
            //{
            //    if(newsItem.FinalRating == null)
            //    {
            //       BackgroundJob.ContinueJobWith(
            //           idHolder,
            //           () => _newsService.RateNewsInDb());
            //    }
            //}
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            await _newsService.RateNewsInDb();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ClearNewsDb")]
        public async Task<IActionResult> ClearNewsDb()
        {
            await _unitOfWork.NewsRepository.ClearAsync();
            return Ok();
        }
    }
}