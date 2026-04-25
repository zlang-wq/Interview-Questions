using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public class Scooter : IParkable, IChargable
{
    public string LicensePlate { get; init; } = "";
    public SpaceType RequiredSpace => SpaceType.Small;
    public int BatteryLevel { get; private set; }

    public void Charge(int kWh)
    {
        BatteryLevel = Math.Min(100, BatteryLevel + kWh);
    }
}