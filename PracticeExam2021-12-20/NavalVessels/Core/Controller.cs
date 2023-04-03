using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private IRepository<IVessel> vessels;
        private IList<ICaptain> captains;
        private string[] existingVesselTypes = {"Submarine", "Battleship" };

        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }
        
        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captain = captains.FirstOrDefault(c => c.FullName == selectedCaptainName);
            if(captain == null)
            {
                return String.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            IVessel vessel = vessels.FindByName(selectedVesselName);
            if(vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            if(vessel.Captain != null)
            {
                return String.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }


            vessel.Captain = captain;
            captain.AddVessel(vessel);

            return String.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            IVessel attackingVessel = vessels.FindByName(attackingVesselName);
            if(attackingVessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }
            IVessel defendingVessel = vessels.FindByName(defendingVesselName);
            if (defendingVessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }

            if(attackingVessel.ArmorThickness == 0)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }

            if(defendingVessel.ArmorThickness == 0)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);
            }

            attackingVessel.Attack(defendingVessel);
            attackingVessel.Captain.IncreaseCombatExperience();
            defendingVessel.Captain.IncreaseCombatExperience();

            return String.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness);
        }

        public string CaptainReport(string captainFullName)
        {
            string result = string.Empty;
            ICaptain captain = captains.FirstOrDefault(c => c.FullName == captainFullName /*&& c.Vessels.Any()*/);
            if (captain != null)
            {
                result = captain.Report();
            }
            return result;
        }

        public string HireCaptain(string fullName)
        {

            if(captains.Any(c=>c.FullName == fullName))
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }

            ICaptain captain = new Captain(fullName);
            captains.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel existingVessel = vessels.Models.FirstOrDefault(v => v.Name == name);
            if (existingVessel != null)
            {
                return String.Format(OutputMessages.VesselIsAlreadyManufactured, existingVessel.GetType().Name, existingVessel.Name);
            }

            if(!existingVesselTypes.Contains(vesselType))
            {
                return OutputMessages.InvalidVesselType;
            }

            IVessel vessel = null!;
            switch(vesselType)
            {
                case "Battleship":
                    vessel = new Battleship(name, mainWeaponCaliber, speed);
                    break;
                case "Submarine":
                    vessel = new Submarine(name, mainWeaponCaliber, speed);
                    break;
            }

            vessels.Add(vessel);
            return String.Format(OutputMessages.SuccessfullyCreateVessel, vessel.GetType().Name, vessel.Name, vessel.MainWeaponCaliber, vessel.Speed);


        }

        public string ServiceVessel(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if(vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }

            vessel.RepairVessel();
            return String.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if(vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }

            if(vessel.GetType() == typeof(Submarine))
            {
                ISubmarine submarine = (Submarine)vessel;
                submarine.ToggleSubmergeMode();
                return String.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }

            if(vessel.GetType() == typeof(Battleship))
            {
                IBattleship battleship = (Battleship)vessel;
                battleship.ToggleSonarMode();
                return String.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);

            }

            return String.Empty;
        }

        public string VesselReport(string vesselName)
        {
            return vessels.FindByName(vesselName).ToString();
        }
    }
}
