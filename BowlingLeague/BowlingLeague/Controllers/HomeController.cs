using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
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
        private IBowlersRepository bowlerRepo { get; set; }
        private ITeamsRepository teamRepo { get; set; }
        
        //Constructor
        public HomeController(IBowlersRepository tempb, ITeamsRepository tempt)
        {
            bowlerRepo = tempb;
            teamRepo = tempt;
        }

        //Call Index and pass list of Bowlers
        public IActionResult Index(int teamId = 0, int pageNum = 1)
        {
            ViewBag.Teams = teamRepo.Teams
                .Where(b => b.TeamID == teamId || teamId == 0)
                .ToList();

            ViewBag.Count = ViewBag.Teams.Count;

            int pageSize = 8;

            var x = new BowlerViewModel
            {                
                Bowlers = bowlerRepo.Bowlers
                .Where(b => b.TeamID == teamId || teamId == 0)
                .OrderBy(b => b.BowlerFirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                BowlerInfo = new BowlerInfo
                {
                    //Gets inmportant Book info - If no category gets info for all books, other wise info is filtered by the selected category
                    TotalNumBowlers = (teamId == 0
                        ? bowlerRepo.Bowlers.Count() : bowlerRepo.Bowlers.Where(x => x.TeamID == teamId).Count()),
                    BowlersPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        public IActionResult Bowlers()
        {

            var x = new BowlerViewModel
            {
                Bowlers = bowlerRepo.Bowlers
                .OrderBy(b => b.TeamID)
                .OrderBy(b => b.BowlerFirstName)// Orders all Bowlers first by team then alphabetical order
            };

            return View(x);
        }

        [HttpGet]
        public IActionResult SignUpForm()
        {
            ViewBag.Teams = teamRepo.Teams.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult SignUpForm(Bowler b)
        {

            bowlerRepo.CreateBowler(b);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditForm(int bowlerId)
        {
            ViewBag.Teams = teamRepo.Teams.ToList();

            var bowler = bowlerRepo.Bowlers.Single(x => x.BowlerID == bowlerId);

            return View("EditForm", bowler);
        }

        [HttpPost]
        public IActionResult EditForm(Bowler b)
        {
            bowlerRepo.SaveBowler(b);

            return RedirectToAction("Bowlers");
        }

        [HttpGet]
        public IActionResult DeletePage(int bowlerId)
        {
            var bowler = bowlerRepo.Bowlers.Single(x => x.BowlerID == bowlerId);

            return View("Delete", bowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            var bowler = bowlerRepo.Bowlers.Single(x => x.BowlerID == b.BowlerID);

            bowlerRepo.DeleteBowler(bowler);

            return RedirectToAction("Bowlers");
        }
    }
}
