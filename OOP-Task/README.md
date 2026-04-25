# Object Oriented Programing Task

## Parking Garage
### Task

There is a parking garage that is `n` levels tall with `m` spaces in each level, out of which there are `k` small spaces.
You have 3 types of vehicles:
 - Car
 - Van
 - Motorcylce
These are the requirements for the parking garage:
 - Motorcycles can only be parked in a small slot.
 - Both cars and vans can not be parked in a small slot.
 - The garage class needs to have a `Park` method that parks the vehicle into a valid and available slot, another method that makes the vehicle leave, freeing up that slot and one that lists all the available spaces left.

For example we have:
n = 2, m = 4, k = 1

1. |MOTO | CAR | CAR |empty|
2. |small|empty|empty|empty|

We add a motorcycle, itt will park it on the second level since that's the only free space for small vehicles.
1. |MOTO| CAR | CAR  |empty|
2. |MOTO|empty|empty |empty|

If we try to add a new motorcycle it won't do anything. If we make the first car leave and make 3 new vans park, then the new state will look like: 
1. |MOTO| VAN | CAR  | VAN |
2. |MOTO| VAN |empty |empty|

### Expanded Task
For the expanded task, we can add electric vehicles like electric cars and scooters. Electric vehicles have a battery and a `Charge` method where you can charge their battery. The `Scooter` uses the small space. We also need to charge for parking fees depending on the parked hours. Van fees are the most expensive while electric car fees are the cheapest. Small vehicles park for FREE.

### Design
We have four major concepts in OOP:
**Abstraction**: A design level concept that is used for hiding complexity behind a simple interface. In this example we know that the garage class will have a `Park` and a `Leave` method, but we don't know about how the parking space selection will be implemented.

**Encapsulation**: An implementation level concept. It is used for bundling data and behavior together and expose only what's necessary. For example for debugging's sake, we set the access for the `ParkingSpace` to `private`, only letting it's own methods like `TryPark` and `Leave` to modify it.

**Inheritance**: A child class derives behaviour from it's parent. A `Car` is a `Vehicle` so is a `Van`. Vehicles have a license plate and can be parked and they also have a parking fee. The problem appears when it is overused. Like for example, an `ElectricCar` extends the `Car` class with things like `battery_life`, a `Motorcycle` does not have to pay a fee, so we need to create a `SmallVehicle` class for it. A `Scooter` is a small vehicle that is also electric, having the same methods like the `ElectricCar`, like `Charge()`. To avoid this it is better to use Composition.
**Composition**: More flexible than inheritance. Instead of `Car` is a `Vehicle`, it has a `Parkable`, `Feeable` maybe `Chargeable` if it's electric. You can build and add behaviours like LEGO bricks. 

**Polymorphism**: Same interface, different behaviour. Like you can charge a parking fee for both a car and electric car, but it is calculated in a diferent way. Like maybe the first hour for an electirc car is cheaper.

For the Implementation we use the S.O.L.I.D. design principle.
**S. Single responsibility**
Each class should do a single responsibility. Vehicle doesn't park itself, space manages occupancy, garage finds spaces.
**O. Open/Closed**
Open for extension, closed for modification. You should be able to add new behavior without touching existing code. Do you want to add a truck? Add a new class that inherits `Vehicle`. You don't touch `ParkingSpace` or the `Garage`.

**L. Liskov Substitution**
This is the weird one. If you have a parent class and a child class, you should be able to replace the parent with the child and nothing should break. If your subclass needs to throw an exception that the parent doesn't, or returns null when the parent always returns a value then you've violated it. For the `Garage` class any Vehicle can go where a Vehicle is expected. If you have a list of Vehicles, you don't care if it's a Car or a Motorcycle. They all have RequiredSpace. None of them throw exceptions or lie about what they are.

**I. Interface Segregation**
Don't force a class to implement methods it doesn't need. We can add `IParkable` or `IFeeable` interfaces for the vehicles.

**D. Dependency Inversion**
High-level modules shouldn't depend on low-level modules. Both should depend on abstractions. The garage doesn't hardcore how many levels it has, it recieve the configuration through the constructor. If the layout changes, the class doesn't care.
One of the mechanism for dependency inversion is dependency injection. There are three flavors.
- **Constructor injection**: you pass it in when you create the object. Most common, most clean.
- **Property injection**: you set it after creation. A bit messy, use it when you can't control the constructor.
- **Method injection**: you pass it as a parameter to a specific method. Rare, but useful for optional dependencies.

### Implementation
For the Implementation we go with an abstract `Vehicle` class then extend it with `Car`, `Van` and `Motorcycle`, setting the required space for each children.

```c#
public enum SpaceType
{
    Regular,
    Small
}

public abstract class Vehicle
{
    public string LicensePlate { get; init; } = "";
    public abstract SpaceType RequiredSpace { get; }
}
```

```c#
public class Car: Vehicle
{
    public override SpaceType RequiredSpace => SpaceType.Regular;
}
```

```c#
public class Motorcycle: Vehicle
{
    public override SpaceType RequiredSpace => SpaceType.Small;
}
```