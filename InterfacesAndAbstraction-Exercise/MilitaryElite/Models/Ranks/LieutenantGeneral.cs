using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private List<Private> privates;
        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, params Private[] privates) : base(id, firstName, lastName, salary)
        {
            Privates = privates.ToList();
        }

        public List<Private> Privates { get { return privates; } private set { privates = value; } }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(base.ToString());

            str.AppendLine("Privates:");


            foreach (Private priv in Privates)
            {
                str.AppendLine($"  {priv.ToString()}");
            }

            return str.ToString().TrimEnd();
        }
    }

}


