using System;
using System.Threading.Tasks;
using HullPainting;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      // part 1
      Console.WriteLine("Running panel painting robot...");

      var robot = new Robot("input.txt");

      Task.Run(() => robot.Run()).Wait();
      var panelsPainted = robot.GetNumPanelsPainted();

      Console.WriteLine($"Painted {panelsPainted} panels");
      Console.WriteLine("Robot finished running...");

      Console.ReadLine();

    }
  }
}
