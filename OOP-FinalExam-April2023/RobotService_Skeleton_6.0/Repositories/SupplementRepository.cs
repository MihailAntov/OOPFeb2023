using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
       private List<ISupplement> models;
        public SupplementRepository()
        {
            models = new List<ISupplement>();
        }

        
        public void AddNew(ISupplement model)
        {
            models.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            return models.FirstOrDefault(s => s.InterfaceStandard == interfaceStandard);
        }

        public IReadOnlyCollection<ISupplement> Models()
        {
            return models.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            ISupplement modelToRemove = models.FirstOrDefault(s => s.GetType().Name == typeName);
            if(modelToRemove == null)
            {
                return false;
            }

            return models.Remove(modelToRemove);
        }
    }
}
