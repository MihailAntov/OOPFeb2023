using Vehicles.Models.Interfaces;

namespace Vehicles.Models;

public class Truck : Vehicle
{
    private const double TruckAirConditioning = 1.6;
    private const double TruckFuelCoefficient = 0.95;
    public Truck(double fuel, double fuelConsumption, double tankCapacity) : base(fuel, fuelConsumption, tankCapacity)
    {
        FuelConsumption += TruckAirConditioning;
    }


    public override string Drive(double km)
    {
        return $"{this.GetType().Name} {base.Drive(km)}";
    }

    public override bool CanRefuel(double liters)
    {
        if (liters <= 0)
        {
            Console.WriteLine("Fuel must be a positive number");
            return false;
        }

        if (Fuel + liters * TruckFuelCoefficient > TankCapacity)
        {
            Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            return false;
        }

        return true;

    }

    public override void Refuel(double liters)
    {
        
        
        if(CanRefuel(liters))
        {
            Fuel += liters * 0.95;
        }
        

    }

    public override string ToString()
    {
        return $"Truck: {Fuel:f2}";
    }
}