using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;
using RepositoryLibrary;
using Serilog;
using Serilog.Core;

namespace APIGoodMoodProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserRepository _userRepository;


        public UserController(DataContext context, UserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing yet =(</returns>
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _context.User.FirstOrDefaultAsync(u => u.ID == id));
        }

        /// <summary>
        /// Post method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            if(user == null) { return BadRequest(); }
            await _userRepository.AddAsync(user);
            Log.Logger.Information($"Info|{DateTime.Now}|User {user.Login} was added to database");
            return Ok(user);
        }

        /// <summary>
        /// Delete method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            User user = _context.User.FirstOrDefault(u => u.ID == id);
            if (user == null) { return BadRequest(); }
            await _userRepository.DeleteAsync(id);
            Log.Logger.Information($"Info|{DateTime.Now}|User {user.Login} was deleted from database");
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(User user, Guid id)
        {
            return Ok();
        }

    }
}