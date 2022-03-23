using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public class EFTeamsRepository : ITeamsRepository
    {
        private BowlersDbContext context { get; set; }

        public EFTeamsRepository(BowlersDbContext temp)
        {
            context = temp;
        }
        public IQueryable<Team> Teams => context.Teams;
    }
}
