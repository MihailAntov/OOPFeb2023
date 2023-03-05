using Vehicles.Models.Interfaces;

namespace Vehicles.Models;

public class Bus : Vehicle
{
    private const double BusAirConditioning = 1.4;
    public Bus(double fuel, double fuelConsumption, double tankCapacity): base(fuel, fuelConsumption, tankCapacity)
    {
        FuelConsumption += BusAirConditioning;
    }
    public override void Refuel(double liters)
    {
        if (CanRefuel(liters))
        {
            Fuel += liters;
        } 
    }

    public override string Drive(double km)
    {
        return $"{this.GetType().Name} {base.Drive(km)}";
    }

    public string DriveEmpty(double km)
    {
        FuelConsumption -= BusAirConditioning;
        string result =  $"{this.GetType().Name} {base.Drive(km)}";
        FuelConsumption += BusAirConditioning;
        return result; 
    }

    public override string ToString()
    {
        return $"Bus: {Fuel:f2}";
    }
}
