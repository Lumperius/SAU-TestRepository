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
using RepositoryLibrary;
using RepositoryLibrary.RepositoryInterface;
using Serilog;
using Serilog.Core;
using UserService.Interfaces;

namespace APIGoodMoodProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserHandler _userHandler;
        private readonly IEncrypter _encrypter;


        public UserController(DataContext context, IUnitOfWork unitOfWork, IUserHandler userHandler, IEncrypter encrypter)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userHandler = userHandler;
            _encrypter = encrypter;
        }
        /// <summary>
        /// Authenticate user and put token and refresh token in database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationRequest request)
        {
            try
            {
                User user = _unitOfWork.UserRepository.GetAllAsync()
                    .Result.FirstOrDefault(u =>
                   u.Login == request.Login
                   && u.Password == _encrypter.EncryptString(request.Password));
                if (user == null)
                    return BadRequest("Incorrect password or login");

                var response = _userHandler.Authenticate(user.ID);
                user.Token = response.Token;
                user.RefreshToken = response.RefreshToken;
                await _unitOfWork.UserRepository.PutAsync(user);
                await _unitOfWork.SaveDBAsync();
                   
                return Ok(response);
            }
            catch(Exception ex)
            {
                Log.Error($"{DateTime.Now}|Error|{ex}");    
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Register user and authenticate him and put token and refresh token in database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync();
                if (users.Any(u => u.Email == request.Email || u.Login == request.Login))
                    return BadRequest("Login or email is occupied");

                User newUser = new User()
                {
                    ID = new Guid(),
                    Login = request.Login,
                    Email = request.Email,
                    Password = _encrypter.EncryptString(request.Password)
                };

                var response = _userHandler.Authenticate(newUser.ID);
                newUser.Token = response.Token;
                newUser.RefreshToken = response.RefreshToken;

                await _unitOfWork.UserRepository.AddAsync(newUser);
                await _unitOfWork.SaveDBAsync();

                return Ok(response);
            }
            catch(Exception ex)
            {
                Log.Error($"{DateTime.Now}|Error|{ex}");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns>user</returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (user == null)
                    return StatusCode(404);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Delete method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                User user = _context.User.FirstOrDefault(u => u.ID == id);
                if (user == null) { return StatusCode(404); }
                await _unitOfWork.UserRepository.DeleteAsync(id);
                Log.Logger.Information($"Info|{DateTime.Now}|User {user.Login} was deleted from database");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}