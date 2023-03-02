using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class Private : Soldier, IPrivate
    {
        private decimal salary;
        public Private(int id, string firstName, string lastName, decimal salary) : base (id, firstName, lastName)
        {
            Salary = salary;
        }
        public decimal Salary { get { return salary; } set { salary = value; } }
        public override string ToString()
        {
            return $"{base.ToString()} Salary: {Salary:f2}";
        }
    }
}
