using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterRaces.Repositories
{
    internal class CarRepository : Repository<ICar>
    {
        public override ICar GetByName(string name)
        {
            return models.FirstOrDefault(c => c.Model == name);
        }
    }
}
