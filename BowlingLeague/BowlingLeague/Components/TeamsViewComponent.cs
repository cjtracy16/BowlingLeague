using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlingLeague.Models;

namespace BowlingLeague.Components
{
    public class TeamsViewComponent : ViewComponent
    {

        private ITeamsRepository teamRepo { get; set; }

        public TeamsViewComponent(ITeamsRepository temp)
        {
            teamRepo = temp;
        }

        public IViewComponentResult Invoke()
        {
            var teams = teamRepo.Teams
                .Distinct()
                .OrderBy(x => x);

            //Send category list to View
            return View(teams);
        }
    }
}
