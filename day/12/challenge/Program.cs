using System;
using Moons;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Starting moon simulation...");

      var simulation = new Simulation("input.txt");
      simulation.Tick(1000);

      Console.WriteLine($"Total energy after 1000 steps: {simulation.TotalEnergy}");

      simulation.Reset();

      var repeatRate = simulation.CalculateRepeatRate();
      Console.WriteLine($"Repeat rate: {repeatRate}");

      Console.WriteLine("Finished moon simulation...");
    }
  }
}
