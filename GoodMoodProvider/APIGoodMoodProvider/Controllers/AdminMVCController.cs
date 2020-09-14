using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace APIGoodMoodProvider.Controllers
{
    public class AdminMVCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}