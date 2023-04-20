using EasterRaces.Models.Races.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterRaces.Repositories
{
    public class RaceRepository : Repository<IRace>
    {
        public override IRace GetByName(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }
    }
}
