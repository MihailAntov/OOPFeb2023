
using System.Collections.Generic;
using System.Linq;

using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : Repository<ISubject>, IRepository<ISubject>
    {

        public ISubject FindById(int id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public ISubject FindByName(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }
    }
}
