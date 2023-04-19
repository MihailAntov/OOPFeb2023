using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private IRepository<ISupplement> supplements;
        private IRepository<IRobot> robots;
        private string[] allowedRobotTypes =
        {
            nameof(DomesticAssistant),
            nameof(IndustrialAssistant)
        };

        private string[] allowedSupplementTypes =
        {
            nameof(SpecializedArm),
            nameof(LaserRadar)
        };

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }
        
        public string CreateRobot(string model, string typeName)
        {
            if(!allowedRobotTypes.Contains(typeName))
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }
            IRobot robot = null!;
            switch(typeName)
            {
                case nameof(IndustrialAssistant):
                    robot = new IndustrialAssistant(model);
                    break;

                case nameof(DomesticAssistant):
                    robot = new DomesticAssistant(model);
                    break;
            }

            robots.AddNew(robot);
            return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            if(!allowedSupplementTypes.Contains(typeName))
            {
                return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            ISupplement supplement = null!;
            switch(typeName)
            {
                case nameof(LaserRadar):
                    supplement = new LaserRadar();
                    break;
                case nameof(SpecializedArm):
                    supplement = new SpecializedArm();
                    break;
            }
            supplements.AddNew(supplement);
            return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);


        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            var eligibleRobots = robots.Models()
                .Where(r => r.InterfaceStandards.Contains(intefaceStandard))
                .OrderByDescending(r=>r.BatteryLevel)
                .ToList();
            if(!eligibleRobots.Any())
            {
                return String.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            int batterySum = eligibleRobots.Sum(r => r.BatteryLevel);
            if(batterySum < totalPowerNeeded)
            {
                return String.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - batterySum);
            }

            int robotCounter = 0;

            for(int i = 0; i < eligibleRobots.Count; i++) 
            {
                IRobot currentRobot = eligibleRobots[i];
                if(currentRobot.BatteryLevel >= totalPowerNeeded)
                {
                    currentRobot.ExecuteService(totalPowerNeeded);
                    totalPowerNeeded = 0;
                    robotCounter++;
                    break;
                }
                else
                {
                    totalPowerNeeded -= currentRobot.BatteryLevel;
                    currentRobot.ExecuteService(currentRobot.BatteryLevel);
                    robotCounter++;
                    continue;
                }
            }

            return String.Format(OutputMessages.PerformedSuccessfully, serviceName, robotCounter);

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var robot in robots.Models()
                .OrderByDescending(r=>r.BatteryLevel)
                .ThenBy(r=>r.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            int counter = 0;


            foreach (IRobot robot in robots.Models().Where(r=>r.Model == model))
            {
                if( 2*robot.BatteryLevel >= robot.BatteryCapacity)
                {
                    continue;
                }
                robot.Eating(minutes);
                counter++;
            }

            return String.Format(OutputMessages.RobotsFed, counter);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models().FirstOrDefault(s => s.GetType().Name == supplementTypeName);
            var eligibleRobots = robots.Models()
                .Where(r => !r.InterfaceStandards.Contains(supplement.InterfaceStandard)
                         && r.Model == model);
            if(!eligibleRobots.Any())
            {
                return string.Format(OutputMessages.AllModelsUpgraded,model);
            }

            IRobot robotToUpgrade = eligibleRobots.FirstOrDefault();
            robotToUpgrade.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);
            return string.Format(OutputMessages.UpgradeSuccessful, robotToUpgrade.Model, supplementTypeName);

        }
    }
}
