namespace ParkingGarage_advanced;

public interface IFeeable
{
    decimal Fee { get; }
    decimal CalculateFee(TimeSpan duration);
}