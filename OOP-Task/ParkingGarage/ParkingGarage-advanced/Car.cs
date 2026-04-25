using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public class Car: IParkable, IFeeable
{
    public decimal Fee => (decimal) 2.2;

    public string LicensePlate { get; init; } = "";
    public SpaceType RequiredSpace => SpaceType.Regular;
    
    public decimal CalculateFee(TimeSpan duration) => Fee * (decimal)duration.TotalHours;
}