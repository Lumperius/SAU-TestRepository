using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using Serilog;

namespace APIGoodMoodProvider.Controllers
{
    [Route("news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IRepository<News> _newsRepository;

        public NewsController(DataContext context, IRepository<News> newsRepository)
        {
            _context = context;
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Get all news in database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("newsList")]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                return Ok(await _newsRepository.GetAllAsync());
            }
            catch(Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest("Whoops");
            }
        }
    }
}