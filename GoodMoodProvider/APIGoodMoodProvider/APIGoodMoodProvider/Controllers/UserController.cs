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
        /// Get method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nothing yet =(</returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _unitOfWork.UserRepository.GetByIdAsync(id));
        }

        /// <summary>
        /// Post method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post(User user)
        {
            if(user == null) { return BadRequest(); }
            await _unitOfWork.UserRepository.AddAsync(user);
            Log.Logger.Information($"Info|{DateTime.Now}|User {user.Login} was added to database");
            return Ok(user);
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
            User user = _context.User.FirstOrDefault(u => u.ID == id);
            if (user == null) { return BadRequest(); }
            await _unitOfWork.UserRepository.DeleteAsync(id);
            Log.Logger.Information($"Info|{DateTime.Now}|User {user.Login} was deleted from database");
            return Ok();
        }
    }
}