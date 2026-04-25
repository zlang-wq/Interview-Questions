using ParkingGarage.Enums;

namespace ParkingGarage;

public class Motorcycle: Vehicle
{
    public override SpaceType RequiredSpace => SpaceType.Small;
}