using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace APIGoodMoodProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly DataContext _context;


        public RefreshTokenController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing yet =(</returns>
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Post method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(string refreshThoken)
        {
            return Ok();
        }

        /// <summary>
        /// Delete method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch()
        {
            return Ok();
        }

    }
}
