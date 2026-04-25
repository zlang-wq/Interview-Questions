using ParkingGarage_advanced.Enums;

namespace ParkingGarage_advanced;

public class Garage
{
    public readonly List<ParkingSpace> Spaces = new();
    public int Levels { get; }
    
    public int SpacesPerLevel { get; }

    public Garage(int levels, int spacesPerLevel, int smallSpacesPerLevel)
    {
        Levels = levels;
        SpacesPerLevel = spacesPerLevel;
        for (var level = 0; level < levels; level++)
        {
            for (var spaceNumber = 0; spaceNumber < spacesPerLevel; spaceNumber++)
            {
                Spaces.Add(new ParkingSpace
                {
                    Level = level,
                    Number = spaceNumber,
                    Type = spaceNumber < smallSpacesPerLevel ? SpaceType.Small : SpaceType.Regular
                });
            }
        }
    }

    public ParkingSpace? GetSpace(int level = 0, int number = 0)
    {
        return Spaces.FirstOrDefault(s => s.Level == level && s.Number == number);
    }

    public IParkable? GetVehicle(int level = 0, int number = 0)
    {
        return Spaces.Where(s => s.Level == level && s.Number == number)
            .Select(s => s.ParkedVehicle)
            .FirstOrDefault();
    }

    public ParkingSpace? Park(IParkable vehicle)
    {
        var space = Spaces.FirstOrDefault(s => !s.IsOccupied && vehicle.RequiredSpace == s.Type);
        var isSuccess= space?.TryPark(vehicle);
        return isSuccess == true ? space : null;
    }

    public bool Leave(int level, int number)
    {
        var space = Spaces.FirstOrDefault(s => s.Level == level && s.Number == number);
        if (space is null || !space.IsOccupied)
        {
            return false;
        }

        space.Leave();
        return true;
    }

    public int AvailableSpaces(SpaceType type) =>
        Spaces.Count(s => !s.IsOccupied && s.Type == type);
}