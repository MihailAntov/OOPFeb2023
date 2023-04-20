using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnlineShop.Common.Constants;
using System.Reflection;


namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private List<IComputer> computers;
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        private string[] allowedComputerTypes = new string[] { nameof(DesktopComputer), nameof(Laptop) };
        public Controller()
        {
            computers = new List<IComputer>();
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            IComputer computer = CheckIfComputerExists(computerId);
            
            if (components.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t=>t.Name==componentType);
            if (type == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            IComponent component = null!;
            switch(componentType)
            {
                case nameof(CentralProcessingUnit):
                    component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(Motherboard):
                    component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(PowerSupply):
                    component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(RandomAccessMemory):
                    component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(SolidStateDrive):
                    component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
                    break;
                case nameof(VideoCard):
                    component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
                    break;
            }
            computer.AddComponent(component);
            components.Add(component);
            return String.Format(SuccessMessages.AddedComponent,type.Name, id, computerId);


        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (computers.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }
            
            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == computerType);
            if (type == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }
            IComputer computer = null!;
            switch(computerType)
            {
                case "Laptop":
                    computer = new Laptop(id, manufacturer, model, price);
                    break;
                case "DesktopComputer":
                    computer = new DesktopComputer(id, manufacturer, model, price);
                    break;
            }

            computers.Add(computer);
            return String.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            IComputer computer = CheckIfComputerExists(computerId);

            if (peripherals.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t=>t.Name == peripheralType);
            if (type == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            IPeripheral peripheral = null!;
            switch(peripheralType)
            {
                case nameof(Headset):
                    peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case nameof(Keyboard):
                    peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case nameof(Monitor):
                    peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;
                case nameof(Mouse):
                    peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
                    break;

            }
            computer.AddPeripheral(peripheral);
            peripherals.Add(peripheral);
            return String.Format(SuccessMessages.AddedPeripheral, type.Name, id, computerId);
        }

        public string BuyBest(decimal budget)
        {
            IComputer computer = computers
                .Where(c=>c.Price <= budget)
                .OrderByDescending(c=>c.OverallPerformance)
                .FirstOrDefault();

            if(computer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            return BuyComputer(computer.Id);
        }

        public string BuyComputer(int id)
        {
            IComputer computer = CheckIfComputerExists(id);
            string result = string.Empty;
            if(computers.Remove(computer))
            {
                result = computer.ToString();
            }

            return result;
        }

        public string GetComputerData(int id)
        {
            IComputer computer = CheckIfComputerExists(id);
            return computer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            IComputer computer = CheckIfComputerExists(computerId);
            string result = string.Empty;
            IComponent component = computer.RemoveComponent(componentType);

            if(components.Remove(component))
            {
                result = String.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
            }
            return result;

        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IComputer computer = CheckIfComputerExists(computerId);
            string result = string.Empty;
            IPeripheral peripheral = computer.RemovePeripheral(peripheralType);

            if (peripherals.Remove(peripheral))
            {
                result = String.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
            }
            return result;
        }

        private IComputer CheckIfComputerExists(int computerId)
        {
            IComputer computer = computers.FirstOrDefault(c => c.Id == computerId);
            if(computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return computer;
        }
    }
}
