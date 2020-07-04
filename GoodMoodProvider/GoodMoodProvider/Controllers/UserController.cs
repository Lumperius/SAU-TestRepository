using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoodMoodProvider.ViesModels;
using GoodMoodProvider.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.Language;


namespace GoodMoodProvider.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(ViesModels.User Model)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();

            var options = optionsBuilder
                    .UseSqlServer(@"Server=DESKTOP-I8BJOOE;Database=GoodNews!db;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;
            using ( UserContext RegData = new UserContext(options))
            {
                ViesModels.User NewUser = new ViesModels.User();
                NewUser.ID = new Guid();
                NewUser.Nickname = Model.Nickname;
                NewUser.Firstname = Model.Firstname;
                NewUser.SecondName = Model.SecondName;
                NewUser.Age = Model.Age;
                NewUser.Gender = Model.Gender;
                NewUser.IsOnline = 1;

                RegData.UserSet.Add(NewUser);

                RegData.SaveChanges();

            }

            return View();
        }
        public IActionResult Test()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();

            var options = optionsBuilder
                    .UseSqlServer(@"Server=DESKTOP-I8BJOOE;Database=GoodNews!db;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;

            using (UserContext RegData = new UserContext(options))
            {
                return Ok(RegData.UserSet);
            }
        }

    }
}