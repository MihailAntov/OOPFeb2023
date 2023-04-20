using EasterRaces.Models.Drivers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterRaces.Repositories
{
    public class DriverRepository : Repository<IDriver>
    {
        public override IDriver GetByName(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }
    }
}
