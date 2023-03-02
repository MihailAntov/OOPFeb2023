using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private List<Repair> repairs;
        public Engineer(int id, string firstName, string lastName, decimal salary, string corps, params Repair[] repairs) : base(id, firstName, lastName, salary, corps)
        {
            Repairs = repairs.ToList();
        }
        public List<Repair> Repairs { get { return repairs; } set { repairs = value; } }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(base.ToString());

            str.AppendLine("Repairs:");


            foreach (Repair repair in Repairs)
            {
                str.AppendLine($"  {repair.ToString()}");
            }

            return str.ToString().TrimEnd();
        }
    }
}
