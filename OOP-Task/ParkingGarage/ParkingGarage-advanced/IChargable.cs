namespace ParkingGarage_advanced;

public interface IChargable
{
    int BatteryLevel { get; }
    void Charge(int kWh);
}