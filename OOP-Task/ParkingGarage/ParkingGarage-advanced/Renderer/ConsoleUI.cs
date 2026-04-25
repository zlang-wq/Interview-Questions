using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced.Renderer;

public static class ConsoleUI
{
    public static void Run(Garage garage)
    {
        while (true)
        {
            Console.Clear();
            RenderLevel(garage);
            Console.WriteLine("--- MENU ---");
            Console.WriteLine("1. Render level");
            Console.WriteLine("2. Park a vehicle");
            Console.WriteLine("3. Display a parked vehicle");
            Console.WriteLine("4. Vehicle leaves");
            Console.WriteLine("5. Exit");
            Console.Write("> ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    RenderLevel(garage);
                    break;
                case "2":
                    ParkVehicle(garage);
                    break;
                case "3":
                    DisplayVehicle(garage);
                    break;
                case "4":
                    VehicleLeaves(garage);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }

    private static void RenderLevel(Garage garage)
    {
        Console.WriteLine("Level:");

        for (var lvl = 0; lvl < garage.Levels; lvl++)
        {
            for (var number = 0; number < garage.SpacesPerLevel; number++)
            {
                var space = garage.GetSpace(lvl, number);
                switch (space?.ParkedVehicle)
                {
                    case Car:
                        Console.Write("C");
                        break;
                    case Van:
                        Console.Write("V");
                        break;
                    case Motorcycle:
                        Console.Write("M");
                        break;
                    case ElectricCar:
                        Console.Write("E");
                        break;
                    case Scooter:
                        Console.Write("S");
                        break;
                    case null:
                        Console.Write(space?.Type == SpaceType.Small ? "░" : "█");
                        break;
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine($"All available small spaces: {garage.AvailableSpaces(SpaceType.Small)}");
        Console.WriteLine($"All available spaces: {garage.AvailableSpaces(SpaceType.Regular)}");
        Console.WriteLine();

        Console.WriteLine("C - Car");
        Console.WriteLine("V - Van");
        Console.WriteLine("E - Electric car");
        Console.WriteLine("M - Motorcycle");
        Console.WriteLine("S - Scooter");
        Console.WriteLine("█ - Space");
        Console.WriteLine("░ - Small space");
    }

    private static void DisplayVehicle(Garage garage)
    {
        Console.Write("Level:");
        var level = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Space number:");
        var number = int.Parse(Console.ReadLine() ?? "0");
        var vehicle = garage.GetVehicle(level - 1, number - 1);
        if (vehicle == null)
        {
            Console.WriteLine("No Parked vehicle.");
        }
        else
        {
            Console.WriteLine($"Type: {vehicle.GetType().Name}");
            Console.WriteLine($"License plate: {vehicle.LicensePlate}");
            Console.WriteLine($"Space size: {vehicle.RequiredSpace.ToString()}");
            var feeableVehicle = vehicle as IFeeable;
            var electricVehicle = vehicle as IChargable;
            if (feeableVehicle != null)
            {
                Console.WriteLine($"Parking price: {feeableVehicle.Fee}");
            }
            else
            {
                Console.WriteLine("Parking price: FREE");
            }

            if (electricVehicle != null)
            {
                Console.WriteLine("Electric: YES");
                Console.WriteLine($"Battery level: {electricVehicle.BatteryLevel}");
            }
            else
            {
                Console.WriteLine("Electric: NO");
            }

            if (feeableVehicle != null)
            {
                Console.WriteLine("1. Pay parking fee.");
            }

            if (electricVehicle != null)
            {
                Console.WriteLine("2. Charge battery.");
            }

            Console.WriteLine("3. Go back");

            var option = int.Parse(Console.ReadLine() ?? "3");

            if (feeableVehicle != null && option == 1)
            {
                Console.WriteLine("How many hours:");
                var parkedHours = int.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine($"Your price for {parkedHours} hours is: {feeableVehicle.CalculateFee(new TimeSpan(parkedHours, 0, 0))} ");
            }

            if (electricVehicle != null && option == 2)
            {
                Console.WriteLine("How long to charge:");
                var charge = int.Parse(Console.ReadLine() ?? "0");
                electricVehicle.Charge(charge);
                Console.WriteLine($"Charged battery level: {electricVehicle.BatteryLevel}");
            }
        }
    }

    private static void ParkVehicle(Garage garage)
    {
        Console.Write("Type c/v/e/m/s (car/van/electric/motorcycle/scooter): ");
        var type = Console.ReadLine()?.Trim().ToLower();

        var licensePlate = Guid.NewGuid().ToString()[..6];
        IParkable vehicle = type switch
        {
            "c" => new Car {LicensePlate = licensePlate},
            "v" => new Van {LicensePlate = licensePlate},
            "e" => new ElectricCar {LicensePlate = licensePlate},
            "m" => new Motorcycle {LicensePlate = licensePlate},
            "s" => new Scooter {LicensePlate = licensePlate},
            _ => throw new InvalidOperationException("Unknown vehicle type")
        };

        var space = garage.Park(vehicle);
        Console.WriteLine(space is not null
            ? $"Parked {vehicle.GetType().Name} at Level {space.Level + 1}, Space {space.Number + 1}"
            : "No available space for this vehicle type.");
    }

    private static void VehicleLeaves(Garage garage)
    {
        Console.Write("Level: ");
        if (!int.TryParse(Console.ReadLine(), out var lvl)) return;
        Console.Write("Space: ");
        if (!int.TryParse(Console.ReadLine(), out var num)) return;

        var isSuccess = garage.Leave(lvl - 1, num - 1);
        Console.WriteLine(isSuccess ? "Vehicle left." : "Nothing parked there.");
    }
}