using System;
using System.Collections.Generic;
using System.Text;
using UserService.Interfaces;
using ModelsLibrary.Responces;
using ModelsLibrary.Requests;
using ModelsLibrary;
using ContextLibrary.DataContexts;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using RepositoryLibrary.RepositoryInterface;

namespace UserService
{
    public class UserHandler : IUserHandler
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Encrypter _encrypter;
        public UserHandler(DataContext context, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _context = context;
            _config = config;
            _unitOfWork = unitOfWork;
            _encrypter = new Encrypter();

        }
        public AuthenticationResponse Authenticate(Guid userId)
        {
            try
            {
                User user = _context.User
                  .Include(u => u.UserRoles)
                  .FirstOrDefault(u => u.ID == userId);

                if (user == null) { return null; }
                var token = BuildToken(user);
                var refreshToken = BuildRefreshToken(user);

                user.Token = token;
                user.RefreshToken = refreshToken;

                var response = new AuthenticationResponse()
                {
                    Id = user.ID,
                    Login = user.Login,
                    Token = token,
                    RefreshToken = refreshToken
                };

                _unitOfWork.UserRepository.PutAsync(user);
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private string BuildToken(User user)
        { 
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D"))
            };
            
            claims.AddRange(user.UserRoles
    .Select(ur => new Claim(ClaimsIdentity.DefaultRoleClaimType, _context.Role
    .FirstOrDefault(r => r.ID == ur.RoleID).Name))?
    
    .ToList());



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User Register(RegistrationRequest request)
        {
            if (request == null) { return null; }
            User user = new User()
            {
                ID = new Guid(),
                Login = request.Login,
                Email = request.Email,
                Password = _encrypter.EncryptString(request.Password)
            };

            return user;
        }


        private string BuildRefreshToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var refreshToken = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        public async Task RefreshToken(string refreshToken, Guid userId)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(refreshToken))
                {
                    var decodedToken = handler.ReadJwtToken(refreshToken);
                    if (decodedToken.ValidTo < DateTime.Now)
                    {
                        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                        Authenticate(userId);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
