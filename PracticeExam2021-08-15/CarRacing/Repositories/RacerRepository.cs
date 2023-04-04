﻿using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarRacing.Utilities.Messages;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private List<IRacer> models;
        public RacerRepository()
        {
            models = new List<IRacer>();
        }
        
        public IReadOnlyCollection<IRacer> Models => models.AsReadOnly();

        public void Add(IRacer model)
        {
            if(model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }
            
            models.Add(model);
        }

        public IRacer FindBy(string property)
        {
           return models.FirstOrDefault(r=>r.Username == property);
        }

        public bool Remove(IRacer model)
        {
            return models.Remove(model);
        }
    }
}
