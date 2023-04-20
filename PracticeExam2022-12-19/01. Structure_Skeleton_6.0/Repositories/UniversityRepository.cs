
using System.Collections.Generic;
using System.Linq;

using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : Repository<IUniversity>, IRepository<IUniversity>
    {
        
        public IUniversity FindById(int id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public IUniversity FindByName(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }
    }
}
