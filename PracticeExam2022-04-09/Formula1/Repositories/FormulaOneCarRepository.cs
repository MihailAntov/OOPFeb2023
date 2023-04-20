using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private List<IFormulaOneCar> models;
        public FormulaOneCarRepository()
        {
            models = new List<IFormulaOneCar>();
        }
        
        public IReadOnlyCollection<IFormulaOneCar> Models => models.AsReadOnly();
        

        public void Add(IFormulaOneCar car)
        {
            models.Add(car);
        }

        public IFormulaOneCar FindByName(string model)
        {
            return models.FirstOrDefault(c => c.Model == model);
        }

        public bool Remove(IFormulaOneCar car)
        {
            return models.Remove(car);
        }
    }
}
