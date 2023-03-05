namespace Vehicles.Models.Interfaces
{
    public interface IVehicle
    {
        string Drive(double km);
        void Refuel(double liters);
    }
}