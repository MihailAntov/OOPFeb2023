using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const double DefaultBattleshipArmorThickness = 300;
        private bool sonarMode;
        public Battleship(string name, double mainWeaponCaliber, double speed) 
            : base(name, mainWeaponCaliber, speed, DefaultBattleshipArmorThickness)
        {
            
            sonarMode = false;
        }
        

        public bool SonarMode { get { return sonarMode; } }

        public void ToggleSonarMode()
        {
            if(!sonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
            }

            sonarMode = !sonarMode;
        }

        public override void RepairVessel()
        {
            ArmorThickness = DefaultBattleshipArmorThickness;
        }

        public override string ToString()
        {
            string sonarIndicator = SonarMode ? "ON" : "OFF"; 
            return base.ToString() + $"{Environment.NewLine} *Sonar mode: {sonarIndicator}";
        }
    }
}
