using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public class ElectricCar : IParkable, IFeeable, IChargable
{
    public int BatteryLevel { get; private set; }
    public decimal Fee => (decimal) 1.2;

    public string LicensePlate { get; init; } = "";

    public SpaceType RequiredSpace => SpaceType.Regular;

    public decimal CalculateFee(TimeSpan duration) => Fee * (decimal) duration.TotalHours;

    public void Charge(int kWh)
    {
        BatteryLevel = Math.Min(100, BatteryLevel + kWh);
    }
}