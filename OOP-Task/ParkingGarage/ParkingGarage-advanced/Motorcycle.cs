using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public class Motorcycle : IParkable
{
    public string LicensePlate { get; init; } = "";
    public SpaceType RequiredSpace => SpaceType.Small;
}