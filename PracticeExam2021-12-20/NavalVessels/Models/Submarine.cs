using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const int DefaultSubmarineArmorThickness = 200;
        private bool submergeMode;
        public Submarine(string name, double mainWeaponCaliber, double speed) 
            : base(name, mainWeaponCaliber, speed, DefaultSubmarineArmorThickness)
        {
            
            submergeMode = false;
        }

        public bool SubmergeMode { get { return submergeMode; } }

        public void ToggleSubmergeMode()
        {
            if(!submergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
            }
            submergeMode = !submergeMode;
        }

        public override void RepairVessel()
        {
            ArmorThickness = DefaultSubmarineArmorThickness;
        }

        public override string ToString()
        {
            string submergeModeIndicator = SubmergeMode ? "ON" : "OFF";
            return base.ToString() + $"{Environment.NewLine} *Submerge mode {submergeModeIndicator}";
        }
    }
}
