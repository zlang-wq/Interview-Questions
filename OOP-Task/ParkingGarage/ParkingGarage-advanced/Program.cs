// See https://aka.ms/new-console-template for more information

using ParkingGarage_advanced;
using ParkingGarage_advanced.Renderer;

Console.WriteLine("Parking Garage 2");

Console.WriteLine("Levels:");
var levels = int.Parse(Console.ReadLine() ?? "0");
Console.WriteLine("Spaces on levels:");
var spaces = int.Parse(Console.ReadLine() ?? "0");
Console.WriteLine("Small spaces on levels:");
var smallSpaces = int.Parse(Console.ReadLine() ?? "0");

var parkingGarage = new Garage(levels, spaces, smallSpaces);
ConsoleUI.Run(parkingGarage);
