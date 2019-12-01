using System;
using System.IO;

namespace challenge
{
  public class Program
  {
    static void Main(string[] args)
    {
      var program = new Program();

      var fuelRequirements = program.ExecuteProgram("input.txt");
      Console.WriteLine($"Base fuel required is: {fuelRequirements.BaseFuel}.");
      Console.WriteLine($"Recursive fuel required is: {fuelRequirements.RecursiveFuel}.");

      Console.ReadLine();
    }

    public FuelRequirements ExecuteProgram(string input)
    {
      var fuelRequirements = new FuelRequirements() { BaseFuel = 0, RecursiveFuel = 0 };
      if (input == String.Empty) return fuelRequirements;

      var fileStream = new FileStream(input, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        while (!streamReader.EndOfStream)
        {
          var line = streamReader.ReadLine();
          var mass = int.Parse(line);

          // calculate base fuel
          var baseFuel = CalculateFuel(mass);

          // calculate the recursive fuel
          var recursiveFuel = CalculateRecursiveFuel(mass);

          fuelRequirements.BaseFuel += baseFuel;
          fuelRequirements.RecursiveFuel += recursiveFuel;

          Console.WriteLine($"- Mass: {mass}, base fuel: {baseFuel}, recursive fuel: {recursiveFuel}");
        }
      }

      return fuelRequirements;
    }

    public int CalculateFuel(int mass)
    {
      return ((int)Math.Floor(mass / 3.0)) - 2;
    }

    public int CalculateRecursiveFuel(int mass)
    {
      return CalculateRecursiveFuelIterator(mass, 0);
    }

    private int CalculateRecursiveFuelIterator(int mass, int recursiveFuel)
    {
      var initialFuel = CalculateFuel(mass);
      if (initialFuel <= 0) return recursiveFuel;
      return CalculateRecursiveFuelIterator(initialFuel, initialFuel + recursiveFuel);
    }
  }

  public class FuelRequirements
  {
    public int BaseFuel { get; set; }
    public int RecursiveFuel { get; set; }
  }
}
