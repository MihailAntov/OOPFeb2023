using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        private List<Mission> missions;
        public Commando(int id, string firstName, string lastName, decimal salary, string corps, params Mission[] missions) : base(id, firstName, lastName, salary, corps)
        {
            this.Missions = missions.ToList();

        }
        public List<Mission> Missions
        {
            get { return missions; }
            private set
            {
                missions = value;
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(base.ToString());

            str.AppendLine("Missions:");


            foreach (Mission mission in Missions)
            {
                str.AppendLine($"  {mission.ToString()}");
            }

            return str.ToString().TrimEnd();
        }
    }
}
