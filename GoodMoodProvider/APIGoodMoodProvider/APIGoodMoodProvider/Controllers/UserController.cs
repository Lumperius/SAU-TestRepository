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
using ModelsLibrary;
using ModelsLibrary.Requests;
using ModelsLibrary.Responces;
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using Serilog;
using Serilog.Core;
using UserService;
using UserService.Interfaces;

namespace APIGoodMoodProvider.Controllers
{
    /// <summary>
    /// Controller for user related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IRepository<User> _userRepository;
        private readonly IUserHandler _userHandler;


        /// <summary>
        /// Contructor for user controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userRepository"></param>
        /// <param name="userHandler"></param>

        public UserController(DataContext context, IRepository<User> userRepository, IUserHandler userHandler)
        {
            _context = context;
            _userRepository = userRepository;
            _userHandler = userHandler;
        }


        /// <summary>
        /// Authenticates user 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route ("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticationRequest request)
        {
            try
            {
                var response = _userHandler.Authenticate(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest("WhoopsieDaisy");
            }
        }
        /// <summary>
        /// Registers user and redirescts him to authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegistrationRequest request)
        {
            try
            {
                var newUser = _userHandler.Register(request);
                var autRequest = new AuthenticationRequest()
                {
                    Login = request.Login,
                    Password = request.Password
                };
                var response =_userHandler.Authenticate(autRequest);
                return Ok(response);
            }
            catch(Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest();
            }
        }


        /// <summary>
        /// Refreshes token if refresh token is viable
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("revokeToken")]
        public IActionResult RevokeToken(AuthenticationRequest request)
        {
            try
            {         
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest();
            }
        }





        /// <summary>
        /// Get method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing yet =(</returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _context.User.FirstOrDefaultAsync(u => u.ID == id));
            }
            catch (Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Post method for [User]
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Post(User user)
        {
            try
            {
            if (user == null) { return BadRequest(); }
            await _userRepository.AddAsync(user);
            Log.Logger.Information($"Info|{DateTime.Now}|User {user.Login} was added to database");
            return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {   
            User user = _context.User.FirstOrDefault(u => u.ID == id);
            if (user == null) { return BadRequest(); }
            await _userRepository.DeleteAsync(id);
            Log.Logger.Information($"Info|{DateTime.Now}|User {user.Login} was deleted from database");
            return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Put method for [User]
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
         
        [HttpPut]
        [Route("put")]
        public async Task<IActionResult> Put(User user)
        {
            try
            {   
            await _userRepository.DeleteAsync(user.ID);
            await _userRepository.AddAsync(user);
            return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error($"Error|{DateTime.Now}|{ex}");
                return BadRequest();
            }
        }
    }
}