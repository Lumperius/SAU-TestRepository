using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using CqsLibrary.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserHandler _userHandler;
        private readonly IEncrypter _encrypter;
        private readonly IMediator _mediator;


        public UserController(DataContext context, IUnitOfWork unitOfWork, IUserHandler userHandler,
                              IEncrypter encrypter, IMediator mediator)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userHandler = userHandler;
            _encrypter = encrypter;
            _mediator = mediator;
        }
        /// <summary>
        /// Authenticate user and put token and refresh token in database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        [EnableCors]
        public async Task<IActionResult> Authenticate(AuthenticationRequest request)
        {
            try
            {
                var users = (await _unitOfWork.UserRepository.GetAllAsync());
                var user = users.FirstOrDefault(u =>
                u.Login == request.Login);
                if(user == null)
                     return BadRequest("Incorrect login");
                if(!(user.Password == _encrypter.EncryptString(request.Password)))
                     return BadRequest("Incorrect password or login");

                var response = _userHandler.Authenticate(user.ID);
                user.Token = response.Token;
                user.RefreshToken = response.RefreshToken;

                
                var query = new GetUserById(user.ID);
                User userWithRoles = await  _mediator.Send(query);
                foreach(UserRole ur in user.UserRoles)
                {
                    var role = await _unitOfWork.RoleRepository.GetByIdAsync(ur.RoleID);
                    response.Roles.Add(role.Name);
                }
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
        /// Cheks if refresh token is expired and refreshes tokens if not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("refresh/{id}")]
        public async Task<IActionResult> RefreshToken(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            _userHandler.RefreshToken(user.RefreshToken, id);
            await _unitOfWork.SaveDBAsync();
            return Ok(user.Token);
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
                await _unitOfWork.UserRepository.AddAsync(newUser);
                var defaultRole = await _context.Role.FirstOrDefaultAsync(r => r.Name == "User");
                await _unitOfWork.UserRoleRepository.AddAsync(new UserRole() { 
                    ID = new Guid(),
                    UserID = newUser.ID,
                    RoleID = defaultRole.ID
                });
                var response = _userHandler.Authenticate(newUser.ID);
                newUser.Token = response.Token;
                newUser.RefreshToken = response.RefreshToken;

                var query = new GetUserById(newUser.ID);
                User userWithRoles = await _mediator.Send(query);
                foreach (UserRole ur in newUser.UserRoles)
                {
                    var role = await _unitOfWork.RoleRepository.GetByIdAsync(ur.RoleID);
                    response.Roles.Add(role.Name);
                }

                await _unitOfWork.UserRepository.PutAsync(newUser);
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
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
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
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
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