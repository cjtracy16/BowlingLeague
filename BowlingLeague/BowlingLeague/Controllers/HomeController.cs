using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository repo { get; set; }
        
        //Constructor
        public HomeController(IBowlersRepository temp)
        {
            repo = temp;
        }

        //Call Index and pass list of Bowlers
        public IActionResult Index()
        {
            var x = repo.Bowlers.ToList();

            return View(x);
        }
    }
}
