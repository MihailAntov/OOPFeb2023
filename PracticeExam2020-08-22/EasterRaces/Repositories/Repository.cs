using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterRaces.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected List<T> models;
        public Repository()
        {
            models = new List<T>();
        }
        public void Add(T model)
        {
            models.Add(model);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return models.AsReadOnly();
        }

        public abstract T GetByName(string name);
        

        public bool Remove(T model)
        {
            return models.Remove(model);
        }
    }
}
