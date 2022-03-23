using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public class EFBowlersRepository : IBowlersRepository
    {
        private BowlersDbContext context { get; set; }

        public EFBowlersRepository(BowlersDbContext temp)
        {
            context = temp;
        }

        public IQueryable<Bowler> Bowlers => context.Bowlers;
    }
}
