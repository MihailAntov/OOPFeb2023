
using System.Collections.Generic;
using System.Linq;

using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : Repository<IStudent>, IRepository<IStudent>
    {
        

        public IStudent FindById(int id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] names = name.Split(" ");
            IStudent result = models.FirstOrDefault(m=>m.FirstName == names[0]);
            
            if(result == null)
            {
                result = models.FirstOrDefault(m=>m.LastName == names[1]);
            }

            return result;
        }
    }
}
