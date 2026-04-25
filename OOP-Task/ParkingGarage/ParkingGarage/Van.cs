using ParkingGarage.Enums;

namespace ParkingGarage;

public class Van: Vehicle
{
    public override SpaceType RequiredSpace => SpaceType.Regular;
}