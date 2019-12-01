using System;
using System.IO;

namespace challenge
{
  public class Program
  {
    static void Main(string[] args)
    {
      var program = new Program();

      var totalFuel = program.ExecuteProgram("input.txt");
      Console.WriteLine($"Fuel required is: {totalFuel}.");

      Console.ReadLine();
    }

    public int ExecuteProgram(string input)
    {
      var totalFuel = 0;
      if (input == String.Empty) return totalFuel;

      var fileStream = new FileStream(input, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        while (!streamReader.EndOfStream)
        {
          var line = streamReader.ReadLine();
          var mass = int.Parse(line);
          var fuel = CalculateFuel(mass);
          totalFuel += fuel;

          Console.WriteLine($"- Mass: {mass}, fuel: {fuel}");
        }
      }

      return totalFuel;
    }

    public int CalculateFuel(int mass)
    {
      return ((int)Math.Floor(mass / 3.0)) - 2;
    }
  }
}
