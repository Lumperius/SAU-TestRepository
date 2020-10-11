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
        [Route("from={from}count={count}")]
        public async Task<IActionResult> Get(int from = 0, int count = 20)
        {
            try
            {
                List<GetNewsResponse> responseList = new List<GetNewsResponse>();
                var query = new GetNewsAll();
                foreach (News news in await _mediator.Send(query))
                {
                    if(news.PlainText != null)
                    {
                        responseList.Add(new GetNewsResponse
                        {
                            Id = news.ID,
                            Article = news.Article,
                            Body = news.Body,
                            Source = news.Source,
                            Rating = news.WordRating
                        });
                    }
                } 
                return Ok(
                    responseList
                    .OrderByDescending(rl => rl.Rating)
                    .Skip(from).ToList()
                    .Take(count)
                        );
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all news from database
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<GetNewsResponse> responseList = new List<GetNewsResponse>();
                var query = new GetNewsAll();
                foreach (News news in await _mediator.Send(query))
                {
                    responseList.Add(new GetNewsResponse
                    {
                        Id = news.ID,
                        Article = news.Article,
                        Body = news.Body,
                        Source = news.Source,
                        Rating = news.WordRating
                    });
                }

                if (responseList.Any())
                    return Ok(responseList);
                else
                    return StatusCode(404);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get news by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var query = new GetNewsById(id);
                var newsList = _mediator.Send(query);
               
                if (newsList == null)
                    return StatusCode(404);
                
                return Ok(newsList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Clean news table of database
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Clear")]
        public async Task<IActionResult> ClearNewsDb()
        {
            try
            {
                await _unitOfWork.NewsRepository.ClearAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}