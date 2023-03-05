using Vehicles.Models.Interfaces;

namespace Vehicles.Models;

public abstract class Vehicle : IVehicle
{
    private double fuel;

    private double fuelConsumption;
    private double tankCapacity;

    public Vehicle(double fuel, double fuelConsumption, double tankCapacity)
    {
        
        FuelConsumption = fuelConsumption;
        TankCapacity = tankCapacity;
        Fuel = fuel > TankCapacity ? 0 : fuel;
    }
    public double Fuel
    {
        get { return fuel; }
        set { fuel = value; }
    }

    public double FuelConsumption
    {
        get { return fuelConsumption; }
        protected set { fuelConsumption = value; }
    }

    public double TankCapacity 
    {
        get { return tankCapacity; }
        protected set 
        { 
            if(value > 0)
            {
                tankCapacity = value;
            }
            else
            {
                tankCapacity = 0;
            }
        }
    }



    public virtual string Drive(double km)
    {
        double fuelNeeded = FuelConsumption * km;
        if (fuelNeeded > Fuel)
        {
            return "needs refueling";

        }

        Fuel -= fuelNeeded;
        return $"travelled {km} km";

    }
    public virtual bool CanRefuel(double liters)
    {
        if (liters <= 0)
        {
            Console.WriteLine("Fuel must be a positive number");
            return false;
        }

        if (Fuel + liters > TankCapacity)
        {
            Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            return false;
        }

        return true;
    }

    public abstract void Refuel(double liters);
    


}
