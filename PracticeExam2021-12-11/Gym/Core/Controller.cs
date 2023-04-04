using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Repositories.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private IRepository<IEquipment> equipment;
        private ICollection<IGym> gyms;
        private string[] allowedGymTypes =
        {
            nameof(BoxingGym),
            nameof(WeightliftingGym)
        };

        private string[] allowedEquipmentTypes =
        {
            nameof(Kettlebell),
            nameof(BoxingGloves)
        };

        private string[] allowedAthleteTypes =
        {
            nameof(Boxer),
            nameof(Weightlifter)
        };

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }
        
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            if(!allowedAthleteTypes.Contains(athleteType))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }
            IAthlete athlete = null!;
            switch (athleteType)
            {
                case nameof(Boxer):
                    athlete = new Boxer(athleteName, motivation, numberOfMedals);
                    break;

                case nameof(Weightlifter):
                    athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
                    break;
            }

            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);
            if(athleteType == nameof(Boxer))
            {
                if(gym.GetType() != typeof(BoxingGym))
                {
                    return OutputMessages.InappropriateGym;
                }
                
            }
            else if(athleteType == nameof(Weightlifter))
            {
                if(gym.GetType() != typeof(WeightliftingGym))
                {
                    return OutputMessages.InappropriateGym;
                }
            }

            gym.AddAthlete(athlete);
            return String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
            


        }

        public string AddEquipment(string equipmentType)
        {
            if(!allowedEquipmentTypes.Contains(equipmentType))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            IEquipment newEquipment = null!;
            switch(equipmentType)
            {
                case nameof(BoxingGloves):
                    newEquipment = new BoxingGloves();
                    break;

                case nameof(Kettlebell):
                    newEquipment = new Kettlebell();
                    break;
            }

            equipment.Add(newEquipment);
            return String.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            if(!allowedGymTypes.Contains(gymType))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            IGym gym = null!;
            switch(gymType)
            {
                case nameof(BoxingGym):
                    gym = new BoxingGym(gymName);
                    break;

                case nameof(WeightliftingGym):
                    gym = new WeightliftingGym(gymName);
                    break;
            }

            gyms.Add(gym);
            return String.Format(OutputMessages.SuccessfullyAdded,gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);
            return String.Format(OutputMessages.EquipmentTotalWeight, gymName,gym.EquipmentWeight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);
            IEquipment equipmentPiece = equipment.FindByType(equipmentType);

            if(equipmentPiece == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }
            gym.AddEquipment(equipmentPiece);
            equipment.Remove(equipmentPiece);

            return String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = gyms.FirstOrDefault(g => g.Name == gymName);
            gym.Exercise();

            return String.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }
    }
}
