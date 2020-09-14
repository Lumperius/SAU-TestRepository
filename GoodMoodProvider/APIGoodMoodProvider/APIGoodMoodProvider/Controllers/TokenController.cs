using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ContextLibrary.DataContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelsLibrary;

namespace APIGoodMoodProvider.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public TokenController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }


        /// <summary>
        /// Get method for [User]
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>Nothing yet =(</returns>
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Post method for [User]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(string token)
        {
            return Ok(token);
        }
        private string BuildToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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

