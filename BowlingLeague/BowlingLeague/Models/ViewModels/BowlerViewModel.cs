using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models.ViewModels
{
    public class BowlerViewModel
    {
        public IQueryable<Bowler> Bowlers { get; set; }
        public BowlerInfo BowlerInfo { get; set; }
    }
}
