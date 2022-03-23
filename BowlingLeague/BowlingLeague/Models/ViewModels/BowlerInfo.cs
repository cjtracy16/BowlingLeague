using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models.ViewModels
{
    public class BowlerInfo
    {
        public int TotalNumBowlers { get; set; }
        public int BowlersPerPage { get; set; }
        public int CurrentPage { get; set; }

        //Figure out how many pages we need + Rounding
        public int TotalPages => (int)Math.Ceiling((double)TotalNumBowlers / BowlersPerPage);
    }
}
