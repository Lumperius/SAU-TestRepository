using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqsLibrary.Queries;
using Hangfire;
using Hangfire.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using NewsUploader.Interfaces;
using RepositoryLibrary.RepositoryInterface;

namespace APIGoodMoodProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.NewsRepository.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpGet("id")]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _mediator.Send(new GetNewsByIdQuery(new Guid()));
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
                "*/5 * * * *");
            RecurringJob.AddOrUpdate(
                "Onliner",
                () => _newsService.LoadNewsInDb("http://Onliner.by/feed"),
                "*/5 * * * *");
            RecurringJob.AddOrUpdate(
                "S13",
                () => _newsService.LoadNewsInDb("https://S13.ru/rss"),
                "*/5 * * * *");

            RecurringJob.AddOrUpdate(
                "BodyParser",
                () => _newsService.GetAllNewsBody(),
                "*/10 * * * *");

            RecurringJob.AddOrUpdate(
                "NewsRater",
                () => _newsService.RateNewsInDb(),
                "*/10 * * * *");

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

        [HttpGet]
        [AllowAnonymous]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            await _newsService.RateNewsInDb();
            return Ok();
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("ClearNewsDb")]
        public async Task<IActionResult> ClearNewsDb()
        {
            await _unitOfWork.NewsRepository.ClearAsync();
            return Ok();
        }
    }
}