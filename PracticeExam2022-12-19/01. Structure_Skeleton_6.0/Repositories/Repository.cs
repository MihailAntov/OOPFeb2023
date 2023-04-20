
using System.Collections.Generic;
using System.Linq;

using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public abstract class Repository<T> 
    {
        protected List<T> models;

        public Repository()
        {
            models = new List<T>();
        }
        public IReadOnlyCollection<T> Models => models.AsReadOnly();

        public void AddModel(T model)
        {
            models.Add(model);
        }

        
    }
}
