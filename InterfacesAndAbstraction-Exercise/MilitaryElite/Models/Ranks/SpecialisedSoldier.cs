using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryElite.Models
{
    public abstract class SpecialisedSoldier :  Private, ISpecialisedSoldier
    {
        private string corps;

        public SpecialisedSoldier(int id, string firstName, string lastName, decimal salary, string corps) : base (id, firstName, lastName, salary)
        {
            Corps = corps;
        }
        public string Corps
        {
            get
            {
                return corps;
            }
            set
            {
                if(value != "Airforces" && value != "Marines")
                {
                    throw new ArgumentException("Invalid coprs!");
                }
                corps = value;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}Corps: {Corps}";
        }
    }
}
