using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vehicles.Models;
using Vehicles.Models.Interfaces;
using Vehicles.Models.Enums;

namespace Vehicles.Core;

public class Engine : IEngine
{
    private IVehicle car;
    private IVehicle truck;
    private IVehicle bus;
    public void Run()
    {
        GenerateVehicle(VehicleType.Car);
        GenerateVehicle(VehicleType.Truck);
        GenerateVehicle(VehicleType.Bus);
        HandleCommands();
        PrintOutput();
    }

    private void GenerateVehicle(VehicleType type)
    {
        string[] carArgs = Console.ReadLine()
            .Split(" ");
        double fuel = double.Parse(carArgs[1]);
        double fuelConsumption = double.Parse(carArgs[2]);
        double tankCapacity = double.Parse(carArgs[3]);

        switch(type)
        {
            case VehicleType.Car:
                car = new Car(fuel, fuelConsumption, tankCapacity);
                break;
            case VehicleType.Truck:
                truck = new Truck(fuel, fuelConsumption, tankCapacity);
                break;
            case VehicleType.Bus:
                bus = new Bus(fuel, fuelConsumption, tankCapacity);
                break;

        }  
    }

    

    private void HandleCommands()
    {
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            HandleCommand(Console.ReadLine());
        }
    }

    private void HandleCommand(string input)
    {
        string[] cmdArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        string command = cmdArgs[0];
        string vehicle = cmdArgs[1];
        double amount = double.Parse(cmdArgs[2]);

        switch (command)
        {
            case "DriveEmpty":
                Console.WriteLine(((Bus)bus).DriveEmpty(amount));
                break;
            case "Drive":
                switch (vehicle)
                {
                    case "Car":
                        Console.WriteLine(car.Drive(amount));
                        break;
                    case "Truck":
                        Console.WriteLine(truck.Drive(amount));
                        break;
                    case "Bus":
                        Console.WriteLine(bus.Drive(amount));
                        break;

                }
                break;
            case "Refuel":
                switch (vehicle)
                {
                    case "Car":
                        car.Refuel(amount);
                        break;
                    case "Truck":
                        truck.Refuel(amount);
                        break;
                    case "Bus":
                        bus.Refuel(amount);
                        break;
                }
                break;
        }
    }

    private void PrintOutput()
    {
        Console.WriteLine(car.ToString());
        Console.WriteLine(truck.ToString());
        Console.WriteLine(bus.ToString());
    }
}
