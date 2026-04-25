using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public class Van : IParkable, IFeeable
{
    public string LicensePlate { get; init; } = "";
    public decimal Fee => (decimal) 2.5;

    public SpaceType RequiredSpace => SpaceType.Regular;

    public decimal CalculateFee(TimeSpan duration) => Fee * (decimal) duration.TotalHours;
}