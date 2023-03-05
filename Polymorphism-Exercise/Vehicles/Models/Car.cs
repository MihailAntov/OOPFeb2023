using Vehicles.Models.Interfaces;

namespace Vehicles.Models;

public class Car : Vehicle
{
    private const double CarAirConditioning = 0.9;
    public Car(double fuel, double fuelConsumption, double tankCapacity) : base(fuel, fuelConsumption,tankCapacity)
    {
        FuelConsumption += CarAirConditioning;
    }

    public override string Drive(double km)
    {
        return $"{this.GetType().Name} {base.Drive(km)}";
    }


    public override void Refuel(double liters)
    {

       

        if (CanRefuel(liters))
        {
            Fuel += liters;
        }


    }

    public override string ToString()
    {
        return $"Car: {Fuel:f2}";
    }
}
