﻿using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {

        private List<IMilitaryUnit> models;

        public UnitRepository()
        {
            models = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => models.AsReadOnly();

        public void AddItem(IMilitaryUnit model)
        {
            models.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            return models.FirstOrDefault(u => u.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IMilitaryUnit unit = FindByName(name);
            if(unit == null)
            {
                return false;
            }

            return models.Remove(unit);
        }
    }
}
