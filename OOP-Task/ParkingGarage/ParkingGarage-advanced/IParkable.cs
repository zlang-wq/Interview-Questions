using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public interface IParkable
{
    public string LicensePlate { get; }
    SpaceType RequiredSpace { get; }
}