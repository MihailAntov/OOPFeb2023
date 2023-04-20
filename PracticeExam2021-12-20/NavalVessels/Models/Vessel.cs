using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NavalVessels.Utilities.Messages;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private double armorThickness;
        private double mainWeaponCaliber;
        private double speed;
        //private double defaultArmorThickness;

        private ICollection<string> targets;

        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
            targets = new HashSet<string>();
        }
        

        
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                }
                name = value;
            }
        }

        public ICaptain Captain 
        {
            get
            {
                return captain;
            }
            set
            {
                if(value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
                }
                captain = value;
            }
        }
        public double ArmorThickness
        {
            get
            {
                return armorThickness;
            }

            set
            {
                armorThickness = value;
                if(armorThickness < 0)
                {
                    armorThickness = 0;
                }
            }
        }

        public double MainWeaponCaliber
        {
            get
            {
                return mainWeaponCaliber;
            }

            protected set
            {
                mainWeaponCaliber = value;

            }
        }

        public double Speed
        {
            get
            {
                return speed;
            }

            protected set
            {
                speed = value;
            }
        }

        

        public ICollection<string> Targets => targets;

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }
            target.ArmorThickness -= this.MainWeaponCaliber;
            Targets.Add(target.Name);
        }

        public abstract void RepairVessel();
        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"- {Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Armor thickness: {ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {MainWeaponCaliber}");
            sb.AppendLine($" *Speed: {Speed} knots");
            string targetsList = targets.Any() ? string.Join(", ", targets) : "None";

            sb.AppendLine($" *Targets: {targetsList}");

            return sb.ToString().TrimEnd();
        }
    }
}
