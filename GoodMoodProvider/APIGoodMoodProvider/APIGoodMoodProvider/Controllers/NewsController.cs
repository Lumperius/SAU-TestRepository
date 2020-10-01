using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IActionResult> GetSome(int count = 50)
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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<GetNewsResponse> responseList = new List<GetNewsResponse>();
            var query = new GetNewsAll();
            foreach (News news in await _mediator.Send(query))
            {
                responseList.Add(new GetNewsResponse
                {
                    Id = news.ID,
                    Article = news.Article,
                    PlainText = news.PlainText,
                    Source = news.Source,
                    Rating = news.FinalRating
                });
            }
            return Ok(responseList);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var query = new GetNewsById(id);
                var user = _mediator.Send(query);
                if (user == null)
                    return StatusCode(404);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("SwitchHangFire")]
        public async Task<IActionResult> SwitchHangFire(bool switcher)
        {

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