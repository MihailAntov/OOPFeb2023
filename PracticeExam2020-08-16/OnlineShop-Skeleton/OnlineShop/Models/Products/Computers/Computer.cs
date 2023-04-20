using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance
        {
            get
            {
                if(components.Count == 0)
                {
                    return base.OverallPerformance;
                }
                double averagePerformanceOfComponents = Components.Any() ? Components.Average(c => c.OverallPerformance) : 0;
                return base.OverallPerformance + averagePerformanceOfComponents;
            }
            
            
            
        }

        public override decimal Price
        {
            get
            {
                decimal componentSum = Components.Any() ? Components.Sum(c => c.Price) : 0;
                decimal peripheralsSum = Peripherals.Any() ? Peripherals.Sum(p => p.Price) : 0;
                return base.Price + componentSum + peripheralsSum;
            }
            
            
        }


        public IReadOnlyCollection<IComponent> Components => components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals  => peripherals.AsReadOnly();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine($"Overall Performance: {OverallPerformance:f2}. Price: {Price:f2} - {this.GetType().Name}: {Manufacturer} {Model} (Id: {Id})");
            sb.AppendLine(base.ToString());
            //sb.AppendLine(String.Format(Common.Constants.SuccessMessages.ComputerComponentsToString, Components.Count));
            sb.AppendLine($" Components ({components.Count}):");
            foreach(var component in components)
            {
                sb.AppendLine($"  {component}");
            }
            double averagePeripheralPerformance = peripherals.Any() ? peripherals.Average(p => p.OverallPerformance) : 0;
            sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({averagePeripheralPerformance:f2}):");
            foreach(var peripheral in peripherals)
            {
                sb.AppendLine($"  {peripheral}");
            }

            return sb.ToString().TrimEnd();

        }

        public void AddComponent(IComponent component)
        {
            IComponent existingComponent = components.FirstOrDefault(c => c.GetType() == component.GetType());
                
            if(existingComponent!= null)
            {
                throw new ArgumentException(String.Format(Common.Constants.ExceptionMessages.ExistingComponent,component.GetType().Name,this.GetType().Name,Id));
            }
            components.Add(component);
            
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            IPeripheral existingPeripheral = peripherals.FirstOrDefault(c => c.GetType() == peripheral.GetType());

            if (existingPeripheral != null)
            {
                throw new ArgumentException(String.Format(Common.Constants.ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, Id));
            }
            peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            IComponent componentToRemove = components
                .FirstOrDefault(c => c.GetType().Name == componentType);

            if(componentToRemove == null)
            {
                throw new ArgumentException(string.Format(Common.Constants.ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, Id));
            }

            components.Remove(componentToRemove);
            return componentToRemove;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            IPeripheral peripheralToRemove = peripherals
                .FirstOrDefault(c => c.GetType().Name == peripheralType);

            if (peripheralToRemove == null)
            {
                throw new ArgumentException(string.Format(Common.Constants.ExceptionMessages.NotExistingPeripheral, peripheralType, this.GetType().Name, Id));
            }

            peripherals.Remove(peripheralToRemove);
            return peripheralToRemove;
        }
    }
}
