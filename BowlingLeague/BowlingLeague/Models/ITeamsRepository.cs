using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    public interface ITeamsRepository
    {
        IQueryable<Team> Teams { get; }

        //void SaveTeam(Team team);
    }
}
