using ParkingGarage.Enums;

namespace ParkingGarage;

public abstract class Vehicle
{
    public string LicensePlate { get; init; } = "";
    public abstract SpaceType RequiredSpace { get; }
}