using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public class ParkingSpace
{
    public int Level { get; init; }
    public int Number { get; init; }
    public SpaceType Type { get; init; }
    public bool IsOccupied => ParkedVehicle != null;
    public IParkable? ParkedVehicle { get; private set; }

    public bool TryPark(IParkable vehicle)
    {
        if (IsOccupied || vehicle.RequiredSpace != Type)
            return false;
        ParkedVehicle = vehicle;
        return true;
    }

    public void Leave()
    {
        ParkedVehicle = null;
    }
}