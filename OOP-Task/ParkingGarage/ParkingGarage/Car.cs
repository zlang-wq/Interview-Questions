using ParkingGarage.Enums;

namespace ParkingGarage;

public class Car: Vehicle
{
    public override SpaceType RequiredSpace => SpaceType.Regular;
}